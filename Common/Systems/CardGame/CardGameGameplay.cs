using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HendecamMod.Common.Systems.CardGame;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Networking.Packets
{
    public partial class CardGameManager
    {
        private static PlayingCard DrawCardWithLuck(List<PlayingCard> deck, float playerLuck, bool isHoldEm)
        {
            if (deck.Count <= 1 || Math.Abs(playerLuck) < 0.01f)
            {
                PlayingCard c = deck[0];
                deck.RemoveAt(0);
                return c;
            }

            float clampedLuck = Math.Clamp(playerLuck, -1f, 1f);
            if (Main.rand.NextFloat() > Math.Abs(clampedLuck) * 0.7f)
            {
                PlayingCard c = deck[0];
                deck.RemoveAt(0);
                return c;
            }

            var ordered = isHoldEm
                ? deck.OrderBy(c => c.PokerRank).ThenBy(c => c.Suit).ToList()
                : deck.OrderBy(c => c.Id).ToList();

            int poolSize = Math.Max(1, ordered.Count / 3);
            PlayingCard chosen = clampedLuck > 0f
                ? ordered[ordered.Count - 1 - Main.rand.Next(poolSize)]
                : ordered[Main.rand.Next(poolSize)];

            deck.Remove(chosen);
            return chosen;
        }

        private float GetSeatLuck(int seatIdx)
        {
            SeatInfo s = Seats[seatIdx];
            if (s.Type == SeatType.Human && s.PlayerWhoAmI >= 0 && s.PlayerWhoAmI < Main.maxPlayers && Main.player[s.PlayerWhoAmI].active)
                return Main.player[s.PlayerWhoAmI].luck;
            if (s.Type == SeatType.House)
                return 0.15f;
            return 0f;
        }

        public void RequestStartGame()
        {
            if (!IsLocalPlayerHost() || GameInProgress)
                return;

            int mySeat = GetLocalControlledSeat();
            if (mySeat >= 0 && Seats[mySeat].StackCopper < BetAmountCopper)
            {
                int needed = BetAmountCopper - Seats[mySeat].StackCopper;
                if (!Main.LocalPlayer.CanAfford(needed))
                {
                    StatusMessage = CardGameLang.Get("Messages.PlayerCannotAffordBuyIn", Main.LocalPlayer.name, CardGameLang.FormatCoins(BetAmountCopper));
                    return;
                }
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestStartGame);
                var sentRigged = HideCheats ? new List<byte>() : RiggedCardIds.ToList();
                packet.Write((byte)sentRigged.Count);
                foreach (byte id in sentRigged)
                    packet.Write(id);
                packet.Send();
                return;
            }

            StartNewRound(HideCheats ? null : RiggedCardIds.ToList());
            SyncStateToClients();
        }

        public void StartNewRound(List<byte> forcedSeat0Cards)
        {
            if (GameInProgress)
                return;

            TableIsOpen = true;
            EliminateBrokeBots();

            var activeSeatIndices = new List<int>();
            for (int i = 0; i < MaxSeats; i++)
            {
                if (CurrentMode != CardGameMode.TexasHoldEm && Seats[i].Type == SeatType.House)
                    Seats[i] = new SeatInfo();

                Seats[i].Hand.Clear();
                Seats[i].HasPassed = false;
                Seats[i].HasForfeited = false;
                Seats[i].IsAllIn = false;
                Seats[i].HasActedThisStreet = false;
                Seats[i].CalledUno = false;
                Seats[i].CurrentStreetBetCopper = 0;
                Seats[i].FinishRank = 0;

                if (Seats[i].Type == SeatType.House && Seats[i].StackCopper < BetAmountCopper * 4)
                    Seats[i].StackCopper = HouseBankrollCopper;
                if (Seats[i].Type == SeatType.Bot && Seats[i].StackCopper < BetAmountCopper)
                    Seats[i].StackCopper = BetAmountCopper;

                if (Seats[i].Type != SeatType.Closed)
                    activeSeatIndices.Add(i);
            }

            if (activeSeatIndices.Count < 2)
            {
                StatusMessage = CardGameLang.Get("Messages.NeedMorePlayers");
                GameInProgress = false;
                return;
            }

            int minRequiredStack = CurrentMode == CardGameMode.TexasHoldEm ? Math.Max(100, BetAmountCopper / 5) : BetAmountCopper;

            foreach (int seatIdx in activeSeatIndices)
            {
                SeatInfo s = Seats[seatIdx];
                if (s.Type == SeatType.Human && s.StackCopper < minRequiredStack)
                {
                    int topUp = BetAmountCopper - s.StackCopper;
                    if (s.PlayerWhoAmI < 0 || s.PlayerWhoAmI >= Main.maxPlayers || !Main.player[s.PlayerWhoAmI].active ||
                        !Main.player[s.PlayerWhoAmI].CanAfford(topUp))
                    {
                        StatusMessage = CardGameLang.Get("Messages.PlayerCannotAffordBuyIn", s.GetDisplayName(seatIdx), CardGameLang.FormatCoins(BetAmountCopper));
                        GameInProgress = false;
                        return;
                    }
                    Main.player[s.PlayerWhoAmI].BuyItem(topUp);
                    s.StackCopper += topUp;
                }
            }

            CenterPile.Clear();
            DisposedPile.Clear();
            LastPlaySeat = -1;
            NextFinishRank = 1;
            PotDistributed = false;
            TotalPotCopper = 0;

            if (CurrentMode == CardGameMode.Uno)
            {
                StartUnoRound(activeSeatIndices, forcedSeat0Cards);
                return;
            }

            RoundDeck = Enumerable.Range(0, 52).Select(id => new PlayingCard((byte)id)).ToList();
            for (int i = RoundDeck.Count - 1; i > 0; i--)
            {
                int swapIdx = Main.rand.Next(i + 1);
                (RoundDeck[i], RoundDeck[swapIdx]) = (RoundDeck[swapIdx], RoundDeck[i]);
            }

            int riggedTargetSeat = activeSeatIndices[0];
            if (!HideCheats && forcedSeat0Cards != null && forcedSeat0Cards.Count > 0)
            {
                int maxRigged = CurrentMode == CardGameMode.TexasHoldEm ? 2 : 13;
                foreach (byte cardId in forcedSeat0Cards.Take(maxRigged))
                {
                    PlayingCard forcedCard = new PlayingCard(cardId);
                    if (RoundDeck.Remove(forcedCard))
                        Seats[riggedTargetSeat].Hand.Add(forcedCard);
                }
            }

            if (CurrentMode == CardGameMode.TexasHoldEm)
            {
                StartHoldEmRound(activeSeatIndices);
                return;
            }

            foreach (int seatIdx in activeSeatIndices)
                Seats[seatIdx].StackCopper -= BetAmountCopper;

            TotalPotCopper = BetAmountCopper * activeSeatIndices.Count;

            int dealCursor = 0;
            while (RoundDeck.Count > 0)
            {
                int seatIdx = activeSeatIndices[dealCursor % activeSeatIndices.Count];
                int maxCards = (52 / activeSeatIndices.Count) + (dealCursor % activeSeatIndices.Count < (52 % activeSeatIndices.Count) ? 1 : 0);
                if (Seats[seatIdx].Hand.Count < maxCards)
                {
                    PlayingCard drawn = DrawCardWithLuck(RoundDeck, GetSeatLuck(seatIdx), isHoldEm: false);
                    Seats[seatIdx].Hand.Add(drawn);
                }
                if (++dealCursor > 200) break;
            }

            foreach (int seatIdx in activeSeatIndices)
                Seats[seatIdx].Hand.Sort();

            GameInProgress = true;

            foreach (int seatIdx in activeSeatIndices)
            {
                if (Seats[seatIdx].Hand.Count(c => c.Rank == CardRank.Two) == 4)
                {
                    Seats[seatIdx].FinishRank = 1;
                    GameInProgress = false;
                    StatusMessage = CardGameLang.Get("Messages.InstantWinFourTwos", Seats[seatIdx].GetDisplayName(seatIdx)) + DistributeRoundPot();
                    return;
                }
            }

            CurrentTurnSeat = activeSeatIndices[0];
            PlayingCard threeOfSpade = new PlayingCard(CardRank.Three, CardSuit.Spade);
            foreach (int seatIdx in activeSeatIndices)
            {
                if (Seats[seatIdx].Hand.Contains(threeOfSpade))
                {
                    CurrentTurnSeat = seatIdx;
                    break;
                }
            }

            StatusMessage = CardGameLang.Get("Messages.GameStartedLead", Seats[CurrentTurnSeat].GetDisplayName(CurrentTurnSeat));
        }
        private static List<PlayingCard> BuildFullUnoDeck(int copyBase = 0)
        {
            List<PlayingCard> deck = new List<PlayingCard>(108);
            int offset0 = (copyBase * 2) * 60;
            int offset1 = (copyBase * 2 + 1) * 60;

            for (int c = 0; c < 4; c++)
            {
                for (int v = 0; v < 15; v++)
                    deck.Add(new PlayingCard((byte)(offset0 + c * 15 + v)));

                for (int v = 1; v <= 12; v++)
                    deck.Add(new PlayingCard((byte)(offset1 + c * 15 + v)));
            }

            for (int i = deck.Count - 1; i > 0; i--)
            {
                int swap = Main.rand.Next(i + 1);
                (deck[i], deck[swap]) = (deck[swap], deck[i]);
            }
            return deck;
        }

        private static void SortUnoHand(List<PlayingCard> hand)
        {
            hand.Sort((a, b) =>
            {
                int colCmp = a.UnoCol.CompareTo(b.UnoCol);
                if (colCmp != 0) return colCmp;
                int valCmp = a.UnoVal.CompareTo(b.UnoVal);
                return valCmp != 0 ? valCmp : a.Id.CompareTo(b.Id);
            });
        }

        private void StartUnoRound(List<int> activeSeatIndices, List<byte> forcedSeat0Cards)
        {
            foreach (int seatIdx in activeSeatIndices)
                Seats[seatIdx].StackCopper -= BetAmountCopper;

            TotalPotCopper = BetAmountCopper * activeSeatIndices.Count;
            RoundDeck = BuildFullUnoDeck(0);

            int riggedTarget = activeSeatIndices[0];
            if (!HideCheats && forcedSeat0Cards != null && forcedSeat0Cards.Count > 0)
            {
                foreach (byte id in forcedSeat0Cards.Take(7))
                {
                    PlayingCard forced = new PlayingCard(id);
                    if (RoundDeck.Remove(forced))
                        Seats[riggedTarget].Hand.Add(forced);
                }
            }

            foreach (int seatIdx in activeSeatIndices)
            {
                while (Seats[seatIdx].Hand.Count < 7 && RoundDeck.Count > 0)
                {
                    Seats[seatIdx].Hand.Add(RoundDeck[0]);
                    RoundDeck.RemoveAt(0);
                }
                SortUnoHand(Seats[seatIdx].Hand);
            }

            int startCardIdx = RoundDeck.FindIndex(c => !c.IsUnoWild && (int)c.UnoVal <= 9);
            if (startCardIdx < 0) startCardIdx = 0;
            PlayingCard startCard = RoundDeck[startCardIdx];
            RoundDeck.RemoveAt(startCardIdx);

            CenterPile.Add(startCard);
            ActiveUnoColor = startCard.UnoCol == UnoColor.Wild ? UnoColor.Red : startCard.UnoCol;
            UnoPlayDirection = 1;
            UnoHasDrawnThisTurn = false;

            DealerSeatIndex = activeSeatIndices[(DealerSeatIndex + 1) % activeSeatIndices.Count];
            GameInProgress = true;
            CurrentTurnSeat = FindNextUnoSeat(DealerSeatIndex, 1);

            StatusMessage = CardGameLang.Get("Messages.UnoStartedLead",
                startCard.GetUnoFullName(),
                Seats[CurrentTurnSeat].GetDisplayName(CurrentTurnSeat));
        }

        private PlayingCard DrawSingleUnoCard()
        {
            if (RoundDeck.Count == 0 && DisposedPile.Count > 0)
            {
                RoundDeck.AddRange(DisposedPile);
                DisposedPile.Clear();
                for (int i = RoundDeck.Count - 1; i > 0; i--)
                {
                    int swap = Main.rand.Next(i + 1);
                    (RoundDeck[i], RoundDeck[swap]) = (RoundDeck[swap], RoundDeck[i]);
                }
            }

            if (RoundDeck.Count == 0)
                RoundDeck = BuildFullUnoDeck(1);

            PlayingCard drawn = RoundDeck[0];
            RoundDeck.RemoveAt(0);
            return drawn;
        }

        private void DrawUnoCardsToSeat(int seatIdx, int count)
        {
            SeatInfo seat = Seats[seatIdx];
            for (int i = 0; i < count; i++)
            {
                PlayingCard card = DrawSingleUnoCard();
                if (!seat.Hand.Contains(card))
                    seat.Hand.Add(card);
            }
            SortUnoHand(seat.Hand);
            if (seat.Hand.Count > 1)
                seat.CalledUno = false;
        }

        public bool CanPlayUnoCard(PlayingCard played, out string reason)
        {
            reason = "";
            if (CenterPile.Count == 0)
                return true;

            PlayingCard top = CenterPile[CenterPile.Count - 1];
            if (played.IsUnoWild || played.UnoCol == ActiveUnoColor || played.UnoVal == top.UnoVal)
                return true;

            reason = CardGameLang.Get("Messages.UnoMustMatch",
                CardGameLang.Get($"UnoColors.{ActiveUnoColor}"),
                top.GetUnoValueName());
            return false;
        }

        private int FindNextUnoSeat(int fromSeat, int steps)
        {
            int cur = fromSeat;
            int dir = UnoPlayDirection >= 0 ? 1 : -1;
            for (int s = 0; s < steps; s++)
            {
                for (int offset = 1; offset <= MaxSeats; offset++)
                {
                    int idx = (cur + offset * dir + MaxSeats * 4) % MaxSeats;
                    if (Seats[idx].IsActiveInRound)
                    {
                        cur = idx;
                        break;
                    }
                }
            }
            return cur;
        }

        public void RequestPlayUnoCard(PlayingCard card, UnoColor chosenWildColor)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.Uno)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestPlayUnoCard);
                packet.Write(card.Id);
                packet.Write((byte)chosenWildColor);
                packet.Send();
                return;
            }

            ExecutePlayUnoCard(CurrentTurnSeat, card, chosenWildColor);
            SyncStateToClients();
        }

        public bool ExecutePlayUnoCard(int seatIdx, PlayingCard card, UnoColor chosenWildColor)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.Uno || seatIdx != CurrentTurnSeat)
                return false;

            SeatInfo seat = Seats[seatIdx];
            if (!seat.IsActiveInRound || !seat.Hand.Contains(card))
                return false;

            if (!CanPlayUnoCard(card, out string error))
            {
                StatusMessage = error;
                return false;
            }

            seat.Hand.Remove(card);
            MoveCenterPileToDisposed();
            CenterPile.Add(card);
            LastPlaySeat = seatIdx;
            UnoHasDrawnThisTurn = false;

            ActiveUnoColor = card.IsUnoWild
                ? (chosenWildColor == UnoColor.Wild ? UnoColor.Red : chosenWildColor)
                : card.UnoCol;

            if (seat.Hand.Count > 1)
                seat.CalledUno = false;

            string pName = seat.GetDisplayName(seatIdx);
            string colorLabel = CardGameLang.Get($"UnoColors.{ActiveUnoColor}");

            if (seat.Hand.Count == 0)
            {
                seat.FinishRank = 1;
                var others = Enumerable.Range(0, MaxSeats)
                    .Where(i => i != seatIdx && Seats[i].Type != SeatType.Closed && !Seats[i].HasForfeited)
                    .OrderBy(i => Seats[i].Hand.Count)
                    .ThenBy(i => Seats[i].Hand.Sum(c => (int)c.UnoVal))
                    .ToList();

                int rank = 2;
                foreach (int otherIdx in others)
                    Seats[otherIdx].FinishRank = Math.Min(4, rank++);

                MoveCenterPileToDisposed();
                GameInProgress = false;
                StatusMessage = CardGameLang.Get("Messages.UnoWonRound", pName, seat.GetTitleBadge(CurrentMode))
                    + DistributeRoundPot()
                    + CardGameLang.Get("Messages.RoundOver");
                return true;
            }

            int activeCount = Enumerable.Range(0, MaxSeats).Count(i => Seats[i].IsActiveInRound);

            switch (card.UnoVal)
            {
                case UnoValue.Skip:
                    {
                        int skipped = FindNextUnoSeat(seatIdx, 1);
                        CurrentTurnSeat = FindNextUnoSeat(seatIdx, 2);
                        StatusMessage = CardGameLang.Get("Messages.UnoPlayedSkip", pName, card.GetUnoFullName(), Seats[skipped].GetDisplayName(skipped));
                        break;
                    }
                case UnoValue.Reverse:
                    {
                        UnoPlayDirection = (sbyte)(-UnoPlayDirection);
                        CurrentTurnSeat = activeCount == 2 ? FindNextUnoSeat(seatIdx, 2) : FindNextUnoSeat(seatIdx, 1);
                        StatusMessage = CardGameLang.Get("Messages.UnoPlayedReverse", pName, card.GetUnoFullName());
                        break;
                    }
                case UnoValue.DrawTwo:
                    {
                        int victim = FindNextUnoSeat(seatIdx, 1);
                        DrawUnoCardsToSeat(victim, 2);
                        CurrentTurnSeat = FindNextUnoSeat(seatIdx, 2);
                        StatusMessage = CardGameLang.Get("Messages.UnoPlayedDrawTwo", pName, card.GetUnoFullName(), Seats[victim].GetDisplayName(victim));
                        break;
                    }
                case UnoValue.Wild:
                    {
                        CurrentTurnSeat = FindNextUnoSeat(seatIdx, 1);
                        StatusMessage = CardGameLang.Get("Messages.UnoPlayedWild", pName, colorLabel);
                        break;
                    }
                case UnoValue.WildDrawFour:
                    {
                        int victim = FindNextUnoSeat(seatIdx, 1);
                        DrawUnoCardsToSeat(victim, 4);
                        CurrentTurnSeat = FindNextUnoSeat(seatIdx, 2);
                        StatusMessage = CardGameLang.Get("Messages.UnoPlayedWildFour", pName, colorLabel, Seats[victim].GetDisplayName(victim));
                        break;
                    }
                default:
                    {
                        CurrentTurnSeat = FindNextUnoSeat(seatIdx, 1);
                        StatusMessage = CardGameLang.Get("Messages.PlayedHand", pName, card.GetUnoFullName());
                        break;
                    }
            }

            return true;
        }

        public void ExecuteUnoDrawOrPass(int seatIdx)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.Uno || seatIdx != CurrentTurnSeat)
                return;

            SeatInfo seat = Seats[seatIdx];
            string pName = seat.GetDisplayName(seatIdx);

            if (!UnoHasDrawnThisTurn)
            {
                PlayingCard drawn = DrawSingleUnoCard();
                seat.Hand.Add(drawn);
                SortUnoHand(seat.Hand);
                seat.CalledUno = false;

                if (CanPlayUnoCard(drawn, out _))
                {
                    UnoHasDrawnThisTurn = true;
                    StatusMessage = CardGameLang.Get("Messages.UnoDrewPlayable", pName);
                }
                else
                {
                    UnoHasDrawnThisTurn = false;
                    CurrentTurnSeat = FindNextUnoSeat(seatIdx, 1);
                    StatusMessage = CardGameLang.Get("Messages.UnoDrewPassed", pName, Seats[CurrentTurnSeat].GetDisplayName(CurrentTurnSeat));
                }
            }
            else
            {
                UnoHasDrawnThisTurn = false;
                CurrentTurnSeat = FindNextUnoSeat(seatIdx, 1);
                StatusMessage = CardGameLang.Get("Messages.PlayerPassed", pName);
            }
        }

        public void RequestCallUno(int targetSeatIdx)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.Uno)
                return;

            int mySeat = GetLocalControlledSeat();
            if (mySeat < 0)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestCallUno);
                packet.Write((byte)targetSeatIdx);
                packet.Send();
                return;
            }

            ExecuteCallUno(mySeat, targetSeatIdx);
            SyncStateToClients();
        }

        public void ExecuteCallUno(int callerSeat, int targetSeat)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.Uno || callerSeat < 0 || callerSeat >= MaxSeats)
                return;
            if (!Seats[callerSeat].IsActiveInRound || targetSeat < 0 || targetSeat >= MaxSeats)
                return;

            string callerName = Seats[callerSeat].GetDisplayName(callerSeat);

            if (callerSeat == targetSeat)
            {
                SeatInfo self = Seats[callerSeat];
                bool canCallBeforePlay = self.Hand.Count == 2 && CurrentTurnSeat == callerSeat && self.Hand.Any(c => CanPlayUnoCard(c, out _));
                bool canCallWithOne = self.Hand.Count == 1;

                if (!self.CalledUno && (canCallWithOne || canCallBeforePlay))
                {
                    self.CalledUno = true;
                    StatusMessage = CardGameLang.Get("Messages.UnoCalledSelf", callerName);
                }
                else
                {
                    DrawUnoCardsToSeat(callerSeat, 2);
                    StatusMessage = CardGameLang.Get("Messages.UnoWrongSelfPenalty", callerName);
                }
                return;
            }

            SeatInfo target = Seats[targetSeat];
            string targetName = target.GetDisplayName(targetSeat);
            if (target.IsActiveInRound && target.Hand.Count == 1 && !target.CalledUno)
            {
                DrawUnoCardsToSeat(targetSeat, 2);
                StatusMessage = CardGameLang.Get("Messages.UnoCaughtOpponent", callerName, targetName);
            }
            else
            {
                DrawUnoCardsToSeat(callerSeat, 2);
                StatusMessage = CardGameLang.Get("Messages.UnoWrongOpponentPenalty", callerName, targetName);
            }
        }

        private void ExecuteUnoBotTurn(int seatIdx)
        {
            SeatInfo bot = Seats[seatIdx];

            for (int i = 0; i < MaxSeats; i++)
            {
                if (i != seatIdx && Seats[i].IsActiveInRound && Seats[i].Hand.Count == 1 && !Seats[i].CalledUno && Main.rand.NextBool(2))
                {
                    ExecuteCallUno(seatIdx, i);
                    SyncStateToClients();
                    return;
                }
            }

            var playable = bot.Hand.Where(c => CanPlayUnoCard(c, out _)).ToList();
            if (playable.Count > 0)
            {
                PlayingCard chosen = playable
                    .OrderBy(c => c.IsUnoWild ? 1 : 0)
                    .ThenByDescending(c => (int)c.UnoVal)
                    .First();

                if (bot.Hand.Count == 2 && Main.rand.NextFloat() < 0.7f)
                    bot.CalledUno = true;

                UnoColor bestWildCol = UnoColor.Red;
                var coloredInHand = bot.Hand.Where(c => !c.IsUnoWild).GroupBy(c => c.UnoCol).OrderByDescending(g => g.Count()).FirstOrDefault();
                if (coloredInHand != null)
                    bestWildCol = coloredInHand.Key;

                ExecutePlayUnoCard(seatIdx, chosen, bestWildCol);
                SyncStateToClients();
                return;
            }

            ExecuteUnoDrawOrPass(seatIdx);
            if (GameInProgress && CurrentTurnSeat == seatIdx && UnoHasDrawnThisTurn)
            {
                var afterDrawPlayable = bot.Hand.Where(c => CanPlayUnoCard(c, out _)).ToList();
                if (afterDrawPlayable.Count > 0)
                {
                    PlayingCard chosen = afterDrawPlayable[0];
                    if (bot.Hand.Count == 2 && Main.rand.NextFloat() < 0.7f)
                        bot.CalledUno = true;
                    ExecutePlayUnoCard(seatIdx, chosen, UnoColor.Red);
                }
                else
                {
                    ExecuteUnoDrawOrPass(seatIdx);
                }
            }
            SyncStateToClients();
        }
        private void StartHoldEmRound(List<int> activeSeatIndices)
        {
            HoldEmStreet = HoldEmStage.PreFlop;
            CurrentStreetHighBetCopper = 0;

            foreach (int seatIdx in activeSeatIndices)
            {
                float luck = GetSeatLuck(seatIdx);
                while (Seats[seatIdx].Hand.Count < 2 && RoundDeck.Count > 0)
                    Seats[seatIdx].Hand.Add(DrawCardWithLuck(RoundDeck, luck, isHoldEm: true));
                Seats[seatIdx].Hand.Sort();
            }

            DealerSeatIndex = activeSeatIndices[(DealerSeatIndex + 1) % activeSeatIndices.Count];
            int sbSeat = activeSeatIndices[(activeSeatIndices.IndexOf(DealerSeatIndex) + 1) % activeSeatIndices.Count];
            int bbSeat = activeSeatIndices[(activeSeatIndices.IndexOf(DealerSeatIndex) + 2) % activeSeatIndices.Count];

            int smallBlind = Math.Max(50, BetAmountCopper / 10);
            int bigBlind = Math.Max(100, BetAmountCopper / 5);

            PostHoldEmChipBet(sbSeat, smallBlind);
            PostHoldEmChipBet(bbSeat, bigBlind);
            CurrentStreetHighBetCopper = Math.Max(Seats[sbSeat].CurrentStreetBetCopper, Seats[bbSeat].CurrentStreetBetCopper);

            GameInProgress = true;
            CurrentTurnSeat = FindNextHoldEmActor(bbSeat);
            StatusMessage = CardGameLang.Get("Messages.GameStartedLead", Seats[CurrentTurnSeat].GetDisplayName(CurrentTurnSeat));
        }

        private int PostHoldEmChipBet(int seatIdx, int chipAmount)
        {
            SeatInfo s = Seats[seatIdx];
            int actual = Math.Min(s.StackCopper, Math.Max(0, chipAmount));
            s.StackCopper -= actual;
            s.CurrentStreetBetCopper += actual;
            TotalPotCopper += actual;
            if (s.StackCopper <= 0)
                s.IsAllIn = true;
            return actual;
        }

        public void RequestHoldEmAction(HoldEmActionType actionType)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestHoldEmAction);
                packet.Write((byte)actionType);
                packet.Send();
                return;
            }

            int mySeat = GetLocalControlledSeat();
            if (mySeat == CurrentTurnSeat)
            {
                ExecuteHoldEmAction(CurrentTurnSeat, actionType);
                SyncStateToClients();
            }
        }

        public void ExecuteHoldEmAction(int seatIdx, HoldEmActionType action)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.TexasHoldEm || seatIdx != CurrentTurnSeat)
                return;

            SeatInfo seat = Seats[seatIdx];
            string pName = seat.GetDisplayName(seatIdx);
            int toCall = Math.Max(0, CurrentStreetHighBetCopper - seat.CurrentStreetBetCopper);

            switch (action)
            {
                case HoldEmActionType.Fold:
                    seat.HasForfeited = true;
                    seat.HasActedThisStreet = true;
                    seat.FinishRank = 4;
                    DisposedPile.AddRange(seat.Hand);
                    DisposedPile.Sort();
                    seat.Hand.Clear();
                    StatusMessage = CardGameLang.Get("Messages.HoldEmFolded", pName);
                    break;

                case HoldEmActionType.CheckOrCall:
                    seat.HasActedThisStreet = true;
                    if (toCall <= 0)
                    {
                        StatusMessage = CardGameLang.Get("Messages.HoldEmChecked", pName);
                    }
                    else
                    {
                        int paid = PostHoldEmChipBet(seatIdx, toCall);
                        StatusMessage = seat.IsAllIn
                            ? CardGameLang.Get("Messages.HoldEmAllIn", pName, CardGameLang.FormatCoins(seat.CurrentStreetBetCopper))
                            : CardGameLang.Get("Messages.HoldEmCalled", pName, CardGameLang.FormatCoins(paid));
                    }
                    break;

                case HoldEmActionType.Raise:
                    int raiseStep = GetRaiseStepCopper();
                    int totalNeeded = toCall + raiseStep;
                    PostHoldEmChipBet(seatIdx, totalNeeded);
                    seat.HasActedThisStreet = true;
                    if (seat.CurrentStreetBetCopper > CurrentStreetHighBetCopper)
                    {
                        CurrentStreetHighBetCopper = seat.CurrentStreetBetCopper;
                        for (int i = 0; i < MaxSeats; i++)
                        {
                            if (i != seatIdx)
                                Seats[i].HasActedThisStreet = false;
                        }
                    }
                    StatusMessage = seat.IsAllIn
                        ? CardGameLang.Get("Messages.HoldEmAllIn", pName, CardGameLang.FormatCoins(seat.CurrentStreetBetCopper))
                        : CardGameLang.Get("Messages.HoldEmRaised", pName, CardGameLang.FormatCoins(CurrentStreetHighBetCopper));
                    break;

                case HoldEmActionType.AllIn:
                    PostHoldEmChipBet(seatIdx, seat.StackCopper);
                    seat.HasActedThisStreet = true;
                    if (seat.CurrentStreetBetCopper > CurrentStreetHighBetCopper)
                    {
                        CurrentStreetHighBetCopper = seat.CurrentStreetBetCopper;
                        for (int i = 0; i < MaxSeats; i++)
                        {
                            if (i != seatIdx)
                                Seats[i].HasActedThisStreet = false;
                        }
                    }
                    StatusMessage = CardGameLang.Get("Messages.HoldEmAllIn", pName, CardGameLang.FormatCoins(seat.CurrentStreetBetCopper));
                    break;
            }

            var contenders = Enumerable.Range(0, MaxSeats).Where(i => Seats[i].IsActiveInRound).ToList();
            if (contenders.Count == 1)
            {
                ResolveHoldEmFoldWin(contenders[0]);
                return;
            }

            AdvanceHoldEmTurn();
        }

        private void ResolveHoldEmFoldWin(int winnerSeat)
        {
            int wonPot = TotalPotCopper;
            Seats[winnerSeat].StackCopper += wonPot;
            Seats[winnerSeat].FinishRank = 1;
            TotalPotCopper = 0;
            PotDistributed = true;
            GameInProgress = false;
            StatusMessage = CardGameLang.Get("Messages.HoldEmPotWonByFold", Seats[winnerSeat].GetDisplayName(winnerSeat), CardGameLang.FormatCoins(wonPot));
            EliminateBrokeBots();
        }

        private void AdvanceHoldEmTurn()
        {
            var canActSeats = Enumerable.Range(0, MaxSeats).Where(i => Seats[i].CanActInHoldEm).ToList();
            bool streetSettled = canActSeats.All(i => Seats[i].HasActedThisStreet && Seats[i].CurrentStreetBetCopper == CurrentStreetHighBetCopper);

            if (streetSettled || canActSeats.Count == 0)
            {
                AdvanceHoldEmStreet();
                return;
            }

            CurrentTurnSeat = FindNextHoldEmActor(CurrentTurnSeat);
        }

        private void AdvanceHoldEmStreet()
        {
            for (int i = 0; i < MaxSeats; i++)
            {
                Seats[i].CurrentStreetBetCopper = 0;
                Seats[i].HasActedThisStreet = false;
            }
            CurrentStreetHighBetCopper = 0;

            while (true)
            {
                if (HoldEmStreet == HoldEmStage.PreFlop)
                {
                    HoldEmStreet = HoldEmStage.Flop;
                    for (int k = 0; k < 3 && RoundDeck.Count > 0; k++)
                    {
                        CenterPile.Add(RoundDeck[0]);
                        RoundDeck.RemoveAt(0);
                    }
                }
                else if (HoldEmStreet == HoldEmStage.Flop)
                {
                    HoldEmStreet = HoldEmStage.Turn;
                    if (RoundDeck.Count > 0)
                    {
                        CenterPile.Add(RoundDeck[0]);
                        RoundDeck.RemoveAt(0);
                    }
                }
                else if (HoldEmStreet == HoldEmStage.Turn)
                {
                    HoldEmStreet = HoldEmStage.River;
                    if (RoundDeck.Count > 0)
                    {
                        CenterPile.Add(RoundDeck[0]);
                        RoundDeck.RemoveAt(0);
                    }
                }
                else
                {
                    HoldEmStreet = HoldEmStage.Showdown;
                    ResolveHoldEmShowdown();
                    return;
                }

                int ableToBet = Enumerable.Range(0, MaxSeats).Count(i => Seats[i].CanActInHoldEm);
                if (ableToBet >= 2)
                {
                    CurrentTurnSeat = FindNextHoldEmActor(DealerSeatIndex);
                    return;
                }
            }
        }

        private void ResolveHoldEmShowdown()
        {
            var contenders = Enumerable.Range(0, MaxSeats).Where(i => Seats[i].IsActiveInRound).ToList();
            if (contenders.Count == 0)
            {
                GameInProgress = false;
                return;
            }

            int bestScore = -1;
            List<int> winners = new List<int>();
            string winningHandName = "";

            foreach (int seatIdx in contenders)
            {
                HoldEmEvaluatedHand eval = HoldEmEvaluator.EvaluateBest(Seats[seatIdx].Hand, CenterPile);
                if (eval.Score > bestScore)
                {
                    bestScore = eval.Score;
                    winners.Clear();
                    winners.Add(seatIdx);
                    winningHandName = eval.DisplayName;
                }
                else if (eval.Score == bestScore)
                {
                    winners.Add(seatIdx);
                }
            }

            int share = TotalPotCopper / Math.Max(1, winners.Count);
            foreach (int w in winners)
            {
                Seats[w].StackCopper += share;
                Seats[w].FinishRank = 1;
            }

            foreach (int c in contenders)
            {
                if (!winners.Contains(c))
                    Seats[c].FinishRank = 4;
            }

            string winnerNames = string.Join(" & ", winners.Select(w => Seats[w].GetDisplayName(w)));
            int awardedPot = TotalPotCopper;
            TotalPotCopper = 0;
            PotDistributed = true;
            GameInProgress = false;

            StatusMessage = CardGameLang.Get("Messages.HoldEmPotWon", winnerNames, CardGameLang.FormatCoins(awardedPot), winningHandName);
            EliminateBrokeBots();
        }

        private int FindNextHoldEmActor(int fromSeat)
        {
            for (int offset = 1; offset <= MaxSeats; offset++)
            {
                int idx = (fromSeat + offset) % MaxSeats;
                if (Seats[idx].CanActInHoldEm)
                    return idx;
            }
            return fromSeat;
        }

        private void ExecuteHoldEmBotTurn(int seatIdx)
        {
            SeatInfo bot = Seats[seatIdx];
            int toCall = Math.Max(0, CurrentStreetHighBetCopper - bot.CurrentStreetBetCopper);
            HoldEmEvaluatedHand eval = HoldEmEvaluator.EvaluateBest(bot.Hand, CenterPile);

            bool hasStrongHand = (int)eval.Category >= (int)HoldEmHandRank.TwoPair ||
                                 (eval.Category == HoldEmHandRank.OnePair && bot.Hand.Max(c => c.PokerRank) >= 11) ||
                                 (HoldEmStreet == HoldEmStage.PreFlop && (bot.Hand[0].PokerRank == bot.Hand[1].PokerRank || bot.Hand.Max(c => c.PokerRank) >= 13));

            if (toCall == 0)
            {
                if (hasStrongHand && Main.rand.NextBool(2) && bot.StackCopper >= GetRaiseStepCopper())
                    ExecuteHoldEmAction(seatIdx, HoldEmActionType.Raise);
                else
                    ExecuteHoldEmAction(seatIdx, HoldEmActionType.CheckOrCall);
            }
            else
            {
                if (hasStrongHand)
                {
                    if (Main.rand.NextBool(3) && bot.StackCopper > toCall + GetRaiseStepCopper())
                        ExecuteHoldEmAction(seatIdx, HoldEmActionType.Raise);
                    else
                        ExecuteHoldEmAction(seatIdx, HoldEmActionType.CheckOrCall);
                }
                else if (toCall <= GetRaiseStepCopper() || (bot.Type == SeatType.House && Main.rand.NextBool(2)))
                {
                    ExecuteHoldEmAction(seatIdx, HoldEmActionType.CheckOrCall);
                }
                else
                {
                    ExecuteHoldEmAction(seatIdx, HoldEmActionType.Fold);
                }
            }

            SyncStateToClients();
        }

        private void MoveCenterPileToDisposed()
        {
            if (CenterPile.Count > 0)
            {
                DisposedPile.AddRange(CenterPile);
                DisposedPile.Sort();
                CenterPile.Clear();
            }
        }

        public bool IsValidPlay(List<PlayingCard> cardsToPlay, out EvaluatedHand evaluatedPlay, out string reason)
        {
            evaluatedPlay = CardHandEvaluator.Evaluate(cardsToPlay, HardMode);
            EvaluatedHand currentPileHand = CardHandEvaluator.Evaluate(CenterPile, HardMode);
            return CardHandEvaluator.CanBeat(evaluatedPlay, currentPileHand, HardMode, out reason);
        }

        private static bool IsRemainingHandIllegalLastSet(List<PlayingCard> remainingHand, out bool isLeftTwo)
        {
            isLeftTwo = false;
            if (remainingHand == null || remainingHand.Count == 0)
                return false;

            if (remainingHand.All(c => c.Rank == CardRank.Two))
            {
                isLeftTwo = true;
                return true;
            }

            if (remainingHand.Count == 4 && remainingHand.All(c => c.Rank == remainingHand[0].Rank))
                return true;

            return false;
        }

        public void RequestPlayCards(List<PlayingCard> selectedCards)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestPlayCards);
                packet.Write((byte)selectedCards.Count);
                foreach (var c in selectedCards)
                    packet.Write(c.Id);
                packet.Send();
                return;
            }

            ExecutePlayCards(CurrentTurnSeat, selectedCards);
            SyncStateToClients();
        }

        public void RequestPassTurn()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestPassTurn);
                packet.Send();
                return;
            }

            if (CurrentMode == CardGameMode.Uno)
                ExecuteUnoDrawOrPass(CurrentTurnSeat);
            else
                ExecutePassTurn(CurrentTurnSeat);

            SyncStateToClients();
        }

        public bool ExecutePlayCards(int seatIndex, List<PlayingCard> cards)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.President || seatIndex != CurrentTurnSeat)
                return false;

            SeatInfo seat = Seats[seatIndex];
            if (!seat.IsActiveInRound || !cards.All(c => seat.Hand.Contains(c)))
                return false;

            if (!IsValidPlay(cards, out EvaluatedHand evaluatedPlay, out string error))
            {
                StatusMessage = error;
                return false;
            }

            foreach (var c in cards)
                seat.Hand.Remove(c);

            MoveCenterPileToDisposed();
            CenterPile = cards.OrderBy(c => c.Id).ToList();
            LastPlaySeat = seatIndex;

            string playerName = seat.GetDisplayName(seatIndex);
            StatusMessage = CardGameLang.Get("Messages.PlayedHand", playerName, evaluatedPlay.DisplayName);

            bool playedTwoAsLast = seat.Hand.Count == 0 && cards.Any(c => c.Rank == CardRank.Two);
            bool playedQuadAsLast = seat.Hand.Count == 0 && evaluatedPlay.Type == CardHandType.FourOfAKind;
            bool leftIllegalRemaining = IsRemainingHandIllegalLastSet(seat.Hand, out bool leftTwoInHand);

            if (playedTwoAsLast || playedQuadAsLast || leftIllegalRemaining)
            {
                if (seat.Hand.Count > 0)
                {
                    DisposedPile.AddRange(seat.Hand);
                    DisposedPile.Sort();
                    seat.Hand.Clear();
                }

                seat.HasForfeited = true;
                seat.HasPassed = true;
                seat.FinishRank = 4;

                bool isTwoPenalty = playedTwoAsLast || leftTwoInHand;
                StatusMessage += " " + CardGameLang.Get(isTwoPenalty ? "Messages.LostLeftTwoLast" : "Messages.LostLeftFourOfAKindLast", playerName);

                var activeAfterPenalty = Enumerable.Range(0, MaxSeats).Where(i => Seats[i].IsActiveInRound).ToList();
                if (activeAfterPenalty.Count <= 1)
                {
                    if (activeAfterPenalty.Count == 1)
                    {
                        int winnerIdx = activeAfterPenalty[0];
                        Seats[winnerIdx].FinishRank = NextFinishRank;
                        StatusMessage += CardGameLang.Get("Messages.WinnerAnnouncement", Seats[winnerIdx].GetDisplayName(winnerIdx), Seats[winnerIdx].GetTitleBadge(CurrentMode));
                    }

                    MoveCenterPileToDisposed();
                    GameInProgress = false;
                    StatusMessage += DistributeRoundPot() + CardGameLang.Get("Messages.RoundOver");
                    return true;
                }

                AdvanceToNextTurn();
                return true;
            }

            if (seat.Hand.Count == 0)
            {
                seat.FinishRank = NextFinishRank++;
                StatusMessage += CardGameLang.Get("Messages.FinishedAsRank", playerName, seat.GetTitleBadge(CurrentMode));

                var remainingSeats = Enumerable.Range(0, MaxSeats).Where(i => Seats[i].IsActiveInRound).ToList();
                if (remainingSeats.Count <= 1)
                {
                    if (remainingSeats.Count == 1)
                        Seats[remainingSeats[0]].FinishRank = NextFinishRank;

                    MoveCenterPileToDisposed();
                    GameInProgress = false;
                    StatusMessage += DistributeRoundPot() + CardGameLang.Get("Messages.RoundOver");
                    return true;
                }
            }

            AdvanceToNextTurn();
            return true;
        }

        public void ExecutePassTurn(int seatIndex)
        {
            if (!GameInProgress || CurrentMode != CardGameMode.President || seatIndex != CurrentTurnSeat)
                return;

            if (CenterPile.Count == 0)
            {
                StatusMessage = CardGameLang.Get("Messages.MustPlayOnLead");
                return;
            }

            Seats[seatIndex].HasPassed = true;
            StatusMessage = CardGameLang.Get("Messages.PlayerPassed", Seats[seatIndex].GetDisplayName(seatIndex));
            AdvanceToNextTurn();
        }

        private void ClearTrickAndLeadFrom(int leaderSeat, string reasonMessage)
        {
            MoveCenterPileToDisposed();
            LastPlaySeat = -1;
            for (int i = 0; i < MaxSeats; i++)
                Seats[i].HasPassed = false;

            CurrentTurnSeat = Seats[leaderSeat].IsActiveInRound
                ? leaderSeat
                : FindNextActiveSeat(leaderSeat, ignorePass: true);

            StatusMessage = CardGameLang.Get("Messages.LeadsNext", reasonMessage, Seats[CurrentTurnSeat].GetDisplayName(CurrentTurnSeat));
        }

        private void AdvanceToNextTurn()
        {
            var contenders = Enumerable.Range(0, MaxSeats)
                .Where(i => Seats[i].IsActiveInRound && !Seats[i].HasPassed)
                .ToList();

            if (contenders.Count == 0 || (contenders.Count == 1 && contenders[0] == LastPlaySeat))
            {
                int nextLeader = LastPlaySeat >= 0 ? LastPlaySeat : CurrentTurnSeat;
                ClearTrickAndLeadFrom(nextLeader, CardGameLang.Get("Messages.EveryonePassedCleared"));
                return;
            }

            CurrentTurnSeat = FindNextActiveSeat(CurrentTurnSeat, ignorePass: false);
        }

        private int FindNextActiveSeat(int fromSeat, bool ignorePass)
        {
            for (int offset = 1; offset <= MaxSeats; offset++)
            {
                int idx = (fromSeat + offset) % MaxSeats;
                if (Seats[idx].IsActiveInRound && (ignorePass || !Seats[idx].HasPassed))
                    return idx;
            }
            return fromSeat;
        }

        private bool WouldMoveCausePenaltyLoss(List<PlayingCard> fullHand, List<PlayingCard> playMove)
        {
            var remaining = fullHand.Where(c => !playMove.Contains(c)).ToList();
            if (remaining.Count == 0)
            {
                var eval = CardHandEvaluator.Evaluate(playMove, HardMode);
                return playMove.Any(c => c.Rank == CardRank.Two) || eval.Type == CardHandType.FourOfAKind;
            }
            return IsRemainingHandIllegalLastSet(remaining, out _);
        }

        private void ExecuteBotTurn(int seatIndex)
        {
            SeatInfo bot = Seats[seatIndex];
            List<PlayingCard> hand = bot.Hand.OrderBy(c => c.Id).ToList();
            var candidates = GenerateAllCandidateHands(hand, HardMode);

            if (CenterPile.Count == 0)
            {
                var leadPlay = candidates
                    .OrderBy(m => WouldMoveCausePenaltyLoss(hand, m) ? 1 : 0)
                    .ThenByDescending(m => m.Count)
                    .ThenBy(m => m.Max(c => c.Id))
                    .FirstOrDefault() ?? new List<PlayingCard> { hand[0] };

                ExecutePlayCards(seatIndex, leadPlay);
            }
            else
            {
                EvaluatedHand pileEval = CardHandEvaluator.Evaluate(CenterPile, HardMode);
                var validMoves = candidates
                    .Where(m => CardHandEvaluator.CanBeat(CardHandEvaluator.Evaluate(m, HardMode), pileEval, HardMode, out _))
                    .OrderBy(m => WouldMoveCausePenaltyLoss(hand, m) ? 1 : 0)
                    .ThenBy(m => CardHandEvaluator.Evaluate(m, HardMode).Type == CardHandType.FourOfAKind ? 1 : 0)
                    .ThenBy(m => m.Max(c => c.Id))
                    .ToList();

                var safeMoves = validMoves.Where(m => !WouldMoveCausePenaltyLoss(hand, m)).ToList();
                if (safeMoves.Count > 0)
                    ExecutePlayCards(seatIndex, safeMoves[0]);
                else
                    ExecutePassTurn(seatIndex);
            }

            SyncStateToClients();
        }

        private List<List<PlayingCard>> GenerateAllCandidateHands(List<PlayingCard> hand, bool hardMode)
        {
            List<List<PlayingCard>> results = hand.Select(c => new List<PlayingCard> { c }).ToList();
            var byRank = hand.GroupBy(c => c.Rank).OrderBy(g => g.Key).ToList();
            var validPairs = new List<List<PlayingCard>>();

            foreach (var g in byRank)
            {
                var list = g.OrderBy(c => c.Id).ToList();
                if (!hardMode)
                {
                    for (int a = 0; a < list.Count; a++)
                    {
                        for (int b = a + 1; b < list.Count; b++)
                        {
                            var pair = new List<PlayingCard> { list[a], list[b] };
                            results.Add(pair);
                            validPairs.Add(pair);
                        }
                    }
                }
                else
                {
                    var black = list.Where(c => !c.IsRed).ToList();
                    var red = list.Where(c => c.IsRed).ToList();
                    if (black.Count == 2)
                    {
                        results.Add(black);
                        validPairs.Add(black);
                    }
                    if (red.Count == 2)
                    {
                        results.Add(red);
                        validPairs.Add(red);
                    }
                }

                if (list.Count >= 3) results.Add(list.Take(3).ToList());
                if (list.Count == 4) results.Add(list.ToList());
            }

            for (int i = 0; i < validPairs.Count; i++)
            {
                for (int j = i + 1; j < validPairs.Count; j++)
                {
                    if (validPairs[i][0].Rank == validPairs[j][0].Rank)
                        continue;
                    if (hardMode && validPairs[i][0].IsRed != validPairs[j][0].IsRed)
                        continue;

                    results.Add(validPairs[i].Concat(validPairs[j]).ToList());
                    if (!hardMode)
                    {
                        for (int k = j + 1; k < validPairs.Count; k++)
                        {
                            if (validPairs[k][0].Rank != validPairs[i][0].Rank && validPairs[k][0].Rank != validPairs[j][0].Rank)
                                results.Add(validPairs[i].Concat(validPairs[j]).Concat(validPairs[k]).ToList());
                        }
                    }
                }
            }

            foreach (var t in byRank.Where(g => g.Count() >= 3))
            {
                foreach (var p in validPairs.Where(p => p[0].Rank != t.Key))
                    results.Add(t.Take(3).Concat(p).ToList());
            }

            if (!hardMode)
            {
                AddStraightCandidates(byRank, results);
            }
            else
            {
                for (int s = 0; s < 4; s++)
                {
                    CardSuit suit = (CardSuit)s;
                    var suitByRank = hand.Where(c => c.Suit == suit).GroupBy(c => c.Rank).OrderBy(g => g.Key).ToList();
                    AddStraightCandidates(suitByRank, results);
                }
            }

            return results;
        }

        private static void AddStraightCandidates(List<IGrouping<CardRank, PlayingCard>> byRank, List<List<PlayingCard>> results)
        {
            for (int len = 3; len <= byRank.Count; len++)
            {
                for (int start = 0; start <= byRank.Count - len; start++)
                {
                    bool consecutive = true;
                    for (int k = 1; k < len; k++)
                    {
                        if ((int)byRank[start + k].Key != (int)byRank[start + k - 1].Key + 1)
                        {
                            consecutive = false;
                            break;
                        }
                    }
                    if (consecutive)
                        results.Add(Enumerable.Range(start, len).Select(idx => byRank[idx].OrderByDescending(c => c.Id).First()).ToList());
                }
            }
        }

        public void SyncStateToClients()
        {
            if (Main.netMode != NetmodeID.Server)
                return;

            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)CardPacketType.SyncFullState);

            packet.Write((byte)CurrentMode);
            packet.Write((byte)HoldEmStreet);
            packet.Write(CurrentStreetHighBetCopper);
            packet.Write((byte)ActiveUnoColor);
            packet.Write(UnoPlayDirection);
            packet.Write(UnoHasDrawnThisTurn);
            packet.Write(TableIsOpen);
            packet.Write(GameInProgress);
            packet.Write(HardMode);
            packet.Write(BetAmountCopper);
            packet.Write(TotalPotCopper);
            packet.Write((sbyte)HostPlayerWhoAmI);
            packet.Write((byte)CurrentTurnSeat);
            packet.Write((sbyte)LastPlaySeat);
            packet.Write((byte)NextFinishRank);
            packet.Write(StatusMessage ?? "");

            packet.Write((byte)CenterPile.Count);
            foreach (var card in CenterPile)
                packet.Write(card.Id);

            packet.Write((byte)DisposedPile.Count);
            foreach (var card in DisposedPile)
                packet.Write(card.Id);

            for (int i = 0; i < MaxSeats; i++)
            {
                packet.Write((byte)Seats[i].Type);
                packet.Write((sbyte)Seats[i].PlayerWhoAmI);
                packet.Write(Seats[i].HasPassed);
                packet.Write(Seats[i].HasForfeited);
                packet.Write(Seats[i].IsAllIn);
                packet.Write(Seats[i].CalledUno);
                packet.Write(Seats[i].StackCopper);
                packet.Write(Seats[i].CurrentStreetBetCopper);
                packet.Write((byte)Seats[i].FinishRank);
                packet.Write(Seats[i].LastKnownName ?? "");
                packet.Write((byte)Seats[i].Hand.Count);
                foreach (var card in Seats[i].Hand)
                    packet.Write(card.Id);
            }

            packet.Send();
        }

        public void HandlePacket(BinaryReader reader, int whoAmI)
        {
            CardPacketType msgType = (CardPacketType)reader.ReadByte();

            switch (msgType)
            {
                case CardPacketType.SyncFullState:
                    CurrentMode = (CardGameMode)reader.ReadByte();
                    HoldEmStreet = (HoldEmStage)reader.ReadByte();
                    CurrentStreetHighBetCopper = reader.ReadInt32();
                    ActiveUnoColor = (UnoColor)reader.ReadByte();
                    UnoPlayDirection = reader.ReadSByte();
                    UnoHasDrawnThisTurn = reader.ReadBoolean();
                    TableIsOpen = reader.ReadBoolean();
                    GameInProgress = reader.ReadBoolean();
                    HardMode = reader.ReadBoolean();
                    BetAmountCopper = reader.ReadInt32();
                    TotalPotCopper = reader.ReadInt32();
                    HostPlayerWhoAmI = reader.ReadSByte();
                    CurrentTurnSeat = reader.ReadByte();
                    LastPlaySeat = reader.ReadSByte();
                    NextFinishRank = reader.ReadByte();
                    StatusMessage = reader.ReadString();

                    int pileCount = reader.ReadByte();
                    CenterPile.Clear();
                    for (int i = 0; i < pileCount; i++)
                        CenterPile.Add(new PlayingCard(reader.ReadByte()));

                    int disposedCount = reader.ReadByte();
                    DisposedPile.Clear();
                    for (int i = 0; i < disposedCount; i++)
                        DisposedPile.Add(new PlayingCard(reader.ReadByte()));

                    for (int i = 0; i < MaxSeats; i++)
                    {
                        Seats[i].Type = (SeatType)reader.ReadByte();
                        Seats[i].PlayerWhoAmI = reader.ReadSByte();
                        Seats[i].HasPassed = reader.ReadBoolean();
                        Seats[i].HasForfeited = reader.ReadBoolean();
                        Seats[i].IsAllIn = reader.ReadBoolean();
                        Seats[i].CalledUno = reader.ReadBoolean();
                        Seats[i].StackCopper = reader.ReadInt32();
                        Seats[i].CurrentStreetBetCopper = reader.ReadInt32();
                        Seats[i].FinishRank = reader.ReadByte();
                        Seats[i].LastKnownName = reader.ReadString();
                        int handCount = reader.ReadByte();
                        Seats[i].Hand.Clear();
                        for (int c = 0; c < handCount; c++)
                            Seats[i].Hand.Add(new PlayingCard(reader.ReadByte()));
                    }
                    break;

                case CardPacketType.RequestConfigureSeat:
                    byte seatIdx = reader.ReadByte();
                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress)
                    {
                        ServerCycleSeat(seatIdx);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestFillBots:
                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress)
                    {
                        ServerSetupBotTestMode(whoAmI);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestSetupHouseMode:
                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress && CurrentMode == CardGameMode.TexasHoldEm)
                    {
                        ServerSetupHouseMode(whoAmI);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestSetGameMode:
                    CardGameMode requestedMode = (CardGameMode)reader.ReadByte();
                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress)
                    {
                        ServerSetGameMode(requestedMode);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestToggleHardMode:
                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress)
                    {
                        HardMode = !HardMode;
                        StatusMessage = CardGameLang.Get(HardMode ? "Messages.HardModeEnabled" : "Messages.HardModeDisabled");
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestSetBetAmount:
                    int requestedBet = reader.ReadInt32();
                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress)
                    {
                        ServerSetBetAmount(requestedBet);
                        SyncStateToClients();
                        BroadcastPendingInvitesWithCurrentBet();
                    }
                    break;

                case CardPacketType.RequestAddBuyIn:
                    int addedCopper = reader.ReadInt32();
                    if (Main.netMode == NetmodeID.Server && !GameInProgress && CurrentMode == CardGameMode.TexasHoldEm)
                    {
                        ServerAddBuyIn(whoAmI, addedCopper);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestSellHeldItem:
                    int soldCopper = reader.ReadInt32();
                    string soldName = reader.ReadString();
                    if (Main.netMode == NetmodeID.Server && !GameInProgress && CurrentMode == CardGameMode.TexasHoldEm)
                    {
                        ServerAddSoldItemFunds(whoAmI, soldCopper, soldName);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestRetreatCashOut:
                    if (Main.netMode == NetmodeID.Server)
                        ServerRetreatCashOut(whoAmI);
                    break;

                case CardPacketType.RequestStartGame:
                    byte riggedCount = reader.ReadByte();
                    var rigged = new List<byte>();
                    for (int i = 0; i < riggedCount; i++)
                        rigged.Add(reader.ReadByte());

                    if (Main.netMode == NetmodeID.Server && whoAmI == HostPlayerWhoAmI && !GameInProgress)
                    {
                        StartNewRound(HideCheats ? null : rigged);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestPlayCards:
                    byte playCount = reader.ReadByte();
                    var played = new List<PlayingCard>();
                    for (int i = 0; i < playCount; i++)
                        played.Add(new PlayingCard(reader.ReadByte()));

                    if (Main.netMode == NetmodeID.Server &&
                        Seats[CurrentTurnSeat].Type == SeatType.Human &&
                        Seats[CurrentTurnSeat].PlayerWhoAmI == whoAmI)
                    {
                        ExecutePlayCards(CurrentTurnSeat, played);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestPlayUnoCard:
                    PlayingCard unoCard = new PlayingCard(reader.ReadByte());
                    UnoColor wildCol = (UnoColor)reader.ReadByte();
                    if (Main.netMode == NetmodeID.Server &&
                        CurrentMode == CardGameMode.Uno &&
                        Seats[CurrentTurnSeat].Type == SeatType.Human &&
                        Seats[CurrentTurnSeat].PlayerWhoAmI == whoAmI)
                    {
                        ExecutePlayUnoCard(CurrentTurnSeat, unoCard, wildCol);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestCallUno:
                    byte targetSeat = reader.ReadByte();
                    if (Main.netMode == NetmodeID.Server && CurrentMode == CardGameMode.Uno)
                    {
                        int callerSeat = -1;
                        for (int i = 0; i < MaxSeats; i++)
                        {
                            if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == whoAmI)
                            {
                                callerSeat = i;
                                break;
                            }
                        }
                        if (callerSeat != -1)
                        {
                            ExecuteCallUno(callerSeat, targetSeat);
                            SyncStateToClients();
                        }
                    }
                    break;

                case CardPacketType.RequestPassTurn:
                    if (Main.netMode == NetmodeID.Server &&
                        Seats[CurrentTurnSeat].Type == SeatType.Human &&
                        Seats[CurrentTurnSeat].PlayerWhoAmI == whoAmI)
                    {
                        if (CurrentMode == CardGameMode.Uno)
                            ExecuteUnoDrawOrPass(CurrentTurnSeat);
                        else
                            ExecutePassTurn(CurrentTurnSeat);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestHoldEmAction:
                    HoldEmActionType holdEmAct = (HoldEmActionType)reader.ReadByte();
                    if (Main.netMode == NetmodeID.Server &&
                        Seats[CurrentTurnSeat].Type == SeatType.Human &&
                        Seats[CurrentTurnSeat].PlayerWhoAmI == whoAmI)
                    {
                        ExecuteHoldEmAction(CurrentTurnSeat, holdEmAct);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestHostFromThrow:
                    Vector2 throwPos = reader.ReadVector2();
                    if (Main.netMode == NetmodeID.Server)
                        ServerHostGameFromThrow(whoAmI, throwPos);
                    break;

                case CardPacketType.SendInvitePrompt:
                    string hostName = reader.ReadString();
                    int inviteBet = reader.ReadInt32();
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        if (IsPlayerSeatedAtTable(Main.myPlayer) || GameInProgress)
                            break;

                        PendingInviteHostName = hostName;
                        PendingInviteBetCopper = inviteBet;
                        BetAmountCopper = inviteBet;
                        HasPendingInvite = true;
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                        ModContent.GetInstance<CardGameUISystem>().OpenInviteUI();
                    }
                    break;

                case CardPacketType.RespondInvitePrompt:
                    bool accepted = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                        ServerHandleInviteResponse(whoAmI, accepted);
                    break;

                case CardPacketType.RequestForfeitRound:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ServerForfeitPlayerByWhoAmI(whoAmI, leftTableCompletely: false);
                        SyncStateToClients();
                    }
                    break;

                case CardPacketType.RequestLeaveTable:
                    if (Main.netMode == NetmodeID.Server)
                        ServerHandlePlayerExit(whoAmI);
                    break;

                case CardPacketType.CloseTableForAll:
                    string closeReason = reader.ReadString();
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ResetTable();
                        StatusMessage = closeReason;
                        Main.NewText(closeReason, Color.OrangeRed);
                        ModContent.GetInstance<CardGameUISystem>().CloseUI();
                    }
                    break;

                case CardPacketType.GrantCoinPayout:
                    int payoutCopper = reader.ReadInt32();
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AwardCoinsToLocalPlayer(Main.LocalPlayer, payoutCopper);
                        SoundEngine.PlaySound(SoundID.CoinPickup);
                    }
                    break;
            }
        }
    }
}