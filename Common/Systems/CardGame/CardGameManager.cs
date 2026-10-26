using System;
using System.Collections.Generic;
using System.Linq;
using HendecamMod.Common.Systems.CardGame;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Networking.Packets
{
    public partial class CardGameManager : ModSystem
    {
        public static CardGameManager Instance => ModContent.GetInstance<CardGameManager>();

        public static bool HideCheats = false;

        public const int MaxSeats = 4;
        public const float InviteRadiusPixels = 800f;
        public const int MinBetCopper = 100;
        public const int MaxBetCopper = 10000000;
        public const int HouseBankrollCopper = 100000000;

        public static readonly int[] BetStepsCopper =
        {
            100,
            500,
            1000,
            2500,
            5000,
            10000,
            25000,
            50000,
            100000,
            250000,
            500000,
            1000000,
            2000000,
            5000000,
            10000000
        };

        public SeatInfo[] Seats = new SeatInfo[MaxSeats];
        public CardGameMode CurrentMode = CardGameMode.President;
        public HoldEmStage HoldEmStreet = HoldEmStage.PreFlop;
        public int CurrentStreetHighBetCopper;
        public int DealerSeatIndex;
        public UnoColor ActiveUnoColor = UnoColor.Red;
        public sbyte UnoPlayDirection = 1;
        public bool UnoHasDrawnThisTurn;

        public int HostPlayerWhoAmI = -1;
        public bool TableIsOpen;
        public bool GameInProgress;
        public bool HardMode;
        public int BetAmountCopper = MinBetCopper;
        public int TotalPotCopper;
        public bool PotDistributed;

        public int CurrentTurnSeat;
        public int LastPlaySeat = -1;
        public List<PlayingCard> CenterPile = new List<PlayingCard>();
        public List<PlayingCard> DisposedPile = new List<PlayingCard>();
        public List<PlayingCard> RoundDeck = new List<PlayingCard>();
        public string StatusMessage = "";
        public int NextFinishRank = 1;

        public HashSet<byte> RiggedCardIds = new HashSet<byte>();

        public bool HasPendingInvite;
        public string PendingInviteHostName = "";
        public int PendingInviteBetCopper = MinBetCopper;

        private int _botThinkTimer;

        public bool IsLocalPlayerHost() => Main.netMode == NetmodeID.SinglePlayer || HostPlayerWhoAmI == Main.myPlayer;

        public int GetRaiseStepCopper() => Math.Max(100, BetAmountCopper / 5);

        public bool IsPlayerSeatedAtTable(int playerWhoAmI)
        {
            if (!TableIsOpen || playerWhoAmI < 0 || playerWhoAmI >= Main.maxPlayers)
                return false;

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == playerWhoAmI)
                    return true;
            }
            return false;
        }

        public int GetLocalControlledSeat()
        {
            if (Seats[CurrentTurnSeat].Type == SeatType.Human && Seats[CurrentTurnSeat].PlayerWhoAmI == Main.myPlayer)
                return CurrentTurnSeat;

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == Main.myPlayer)
                    return i;
            }
            return -1;
        }

        public bool IsLocalPlayerLowOnFunds()
        {
            int mySeat = GetLocalControlledSeat();
            if (mySeat < 0 || BetAmountCopper <= 0)
                return false;

            int stack = Seats[mySeat].StackCopper;
            if (CurrentMode == CardGameMode.TexasHoldEm)
                return stack < BetAmountCopper;

            int neededFromInventory = Math.Max(0, BetAmountCopper - stack);
            return neededFromInventory > 0 && !Main.LocalPlayer.CanAfford(neededFromInventory);
        }

        public bool ValidateLocalPlayAttempt(List<PlayingCard> selectedCards, out string reason)
        {
            if (!GameInProgress)
            {
                reason = CardGameLang.Get("Messages.GameNotStarted");
                return false;
            }

            int mySeat = GetLocalControlledSeat();
            if (mySeat == -1 || mySeat != CurrentTurnSeat)
            {
                reason = CardGameLang.Get("Messages.NotYourTurn", Seats[CurrentTurnSeat].GetDisplayName(CurrentTurnSeat));
                return false;
            }

            if (selectedCards == null || selectedCards.Count == 0)
            {
                reason = CardGameLang.Get("Messages.SelectCardsFirst");
                return false;
            }

            if (CurrentMode == CardGameMode.Uno)
            {
                if (selectedCards.Count != 1)
                {
                    reason = CardGameLang.Get("Messages.UnoSelectSingleCard");
                    return false;
                }
                if (!CanPlayUnoCard(selectedCards[0], out reason))
                    return false;
                return true;
            }

            return IsValidPlay(selectedCards, out _, out reason);
        }

        public override void OnModLoad() => ResetTable();
        public override void PostSetupContent() => ResetTable();
        public override void ClearWorld() => ResetTable();

        public void ResetTable()
        {
            for (int i = 0; i < MaxSeats; i++)
                Seats[i] = new SeatInfo();

            HostPlayerWhoAmI = (Main.netMode != NetmodeID.Server && Main.myPlayer >= 0 && Main.myPlayer < Main.maxPlayers)
                ? Main.myPlayer
                : -1;

            if (HostPlayerWhoAmI >= 0)
            {
                Seats[0].Type = SeatType.Human;
                Seats[0].PlayerWhoAmI = HostPlayerWhoAmI;
            }

            CurrentMode = CardGameMode.President;
            HoldEmStreet = HoldEmStage.PreFlop;
            CurrentStreetHighBetCopper = 0;
            ActiveUnoColor = UnoColor.Red;
            UnoPlayDirection = 1;
            UnoHasDrawnThisTurn = false;
            TableIsOpen = false;
            GameInProgress = false;
            HardMode = false;
            BetAmountCopper = MinBetCopper;
            TotalPotCopper = 0;
            PotDistributed = false;
            CurrentTurnSeat = 0;
            LastPlaySeat = -1;
            CenterPile.Clear();
            DisposedPile.Clear();
            RoundDeck.Clear();
            NextFinishRank = 1;
            HasPendingInvite = false;
            StatusMessage = CardGameLang.Get("Messages.HostReady");
        }

        public override void PostUpdateEverything()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !TableIsOpen)
                return;

            if (HostPlayerWhoAmI < 0 || HostPlayerWhoAmI >= Main.maxPlayers || !Main.player[HostPlayerWhoAmI].active)
            {
                ServerEndTableBecauseHostLeft();
                return;
            }

            if (!GameInProgress)
                return;

            for (int i = 0; i < MaxSeats; i++)
            {
                SeatInfo s = Seats[i];
                if (s.Type == SeatType.Human && !s.HasForfeited &&
                    (s.PlayerWhoAmI < 0 || s.PlayerWhoAmI >= Main.maxPlayers || !Main.player[s.PlayerWhoAmI].active))
                {
                    ServerForfeitSeat(i, s.GetDisplayName(i), leftTableCompletely: true);
                    SyncStateToClients();
                    if (!GameInProgress)
                        return;
                }
            }

            SeatInfo currentSeat = Seats[CurrentTurnSeat];
            if ((currentSeat.Type == SeatType.Bot || currentSeat.Type == SeatType.House) && currentSeat.IsActiveInRound)
            {
                if (++_botThinkTimer >= 42)
                {
                    _botThinkTimer = 0;
                    if (CurrentMode == CardGameMode.TexasHoldEm)
                        ExecuteHoldEmBotTurn(CurrentTurnSeat);
                    else if (CurrentMode == CardGameMode.Uno)
                        ExecuteUnoBotTurn(CurrentTurnSeat);
                    else
                        ExecuteBotTurn(CurrentTurnSeat);
                }
            }
            else
            {
                _botThinkTimer = 0;
            }
        }

        public static void AwardCoinsToLocalPlayer(Player player, int totalCopper)
        {
            if (player == null || !player.active || totalCopper <= 0)
                return;

            int plat = totalCopper / 1000000;
            int gold = (totalCopper / 10000) % 100;
            int silver = (totalCopper / 100) % 100;
            int copper = totalCopper % 100;

            var source = player.GetSource_Misc("CardGamePayout");
            if (plat > 0) player.QuickSpawnItem(source, ItemID.PlatinumCoin, plat);
            if (gold > 0) player.QuickSpawnItem(source, ItemID.GoldCoin, gold);
            if (silver > 0) player.QuickSpawnItem(source, ItemID.SilverCoin, silver);
            if (copper > 0) player.QuickSpawnItem(source, ItemID.CopperCoin, copper);
        }

        private void SendCoinsToHumanSeat(int seatIdx, int copperAmount)
        {
            if (seatIdx < 0 || seatIdx >= MaxSeats || copperAmount <= 0)
                return;

            SeatInfo seat = Seats[seatIdx];
            if (seat.Type != SeatType.Human || seat.PlayerWhoAmI < 0 || seat.PlayerWhoAmI >= Main.maxPlayers)
                return;

            Player target = Main.player[seat.PlayerWhoAmI];
            if (!target.active)
                return;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                AwardCoinsToLocalPlayer(target, copperAmount);
                SoundEngine.PlaySound(SoundID.CoinPickup);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.GrantCoinPayout);
                packet.Write(copperAmount);
                packet.Send(toClient: seat.PlayerWhoAmI);
            }
        }

        private void EliminateBrokeBots()
        {
            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Bot && Seats[i].StackCopper <= 0)
                {
                    string botName = Seats[i].GetDisplayName(i);
                    Seats[i] = new SeatInfo();
                    StatusMessage += " " + CardGameLang.Get("Messages.BotBustedLeft", botName);
                }
            }
        }

        private void RefundAllHoldEmStacksToPlayers()
        {
            for (int i = 0; i < MaxSeats; i++)
            {
                SeatInfo s = Seats[i];
                if (s.Type == SeatType.Human && s.StackCopper > 0)
                {
                    SendCoinsToHumanSeat(i, s.StackCopper);
                    s.StackCopper = 0;
                }
                else if (s.Type == SeatType.Bot)
                {
                    s.StackCopper = BetAmountCopper;
                }
            }
        }

        private string DistributeRoundPot()
        {
            if (PotDistributed || TotalPotCopper <= 0)
                return "";

            PotDistributed = true;

            int presSeat = -1;
            int viceSeat = -1;

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].HasForfeited)
                    continue;
                if (Seats[i].FinishRank == 1 && presSeat == -1)
                    presSeat = i;
                else if (Seats[i].FinishRank == 2 && viceSeat == -1)
                    viceSeat = i;
            }

            if (presSeat == -1)
                return "";

            int presShare = (TotalPotCopper * 3) / 4;
            int viceShare = TotalPotCopper - presShare;

            string resultMsg;
            if (viceSeat != -1)
            {
                if (Seats[presSeat].Type == SeatType.Human)
                    SendCoinsToHumanSeat(presSeat, presShare);
                else
                    Seats[presSeat].StackCopper += presShare;

                if (Seats[viceSeat].Type == SeatType.Human)
                    SendCoinsToHumanSeat(viceSeat, viceShare);
                else
                    Seats[viceSeat].StackCopper += viceShare;

                resultMsg = " " + CardGameLang.Get("Messages.PayoutSplit",
                    Seats[presSeat].GetDisplayName(presSeat),
                    CardGameLang.FormatCoins(presShare),
                    Seats[viceSeat].GetDisplayName(viceSeat),
                    CardGameLang.FormatCoins(viceShare));
            }
            else
            {
                if (Seats[presSeat].Type == SeatType.Human)
                    SendCoinsToHumanSeat(presSeat, TotalPotCopper);
                else
                    Seats[presSeat].StackCopper += TotalPotCopper;

                resultMsg = " " + CardGameLang.Get("Messages.PayoutSoloPresident",
                    Seats[presSeat].GetDisplayName(presSeat),
                    CardGameLang.FormatCoins(TotalPotCopper));
            }

            TotalPotCopper = 0;
            EliminateBrokeBots();
            return resultMsg;
        }

        public void RequestSetGameMode(CardGameMode newMode)
        {
            if (!IsLocalPlayerHost() || GameInProgress || CurrentMode == newMode)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestSetGameMode);
                packet.Write((byte)newMode);
                packet.Send();
                return;
            }

            ServerSetGameMode(newMode);
            SyncStateToClients();
        }

        private void ServerSetGameMode(CardGameMode newMode)
        {
            bool hadStocks = Seats.Any(s => s.Type == SeatType.Human && s.StackCopper > 0);
            RefundAllHoldEmStacksToPlayers();

            CurrentMode = newMode;
            CenterPile.Clear();
            DisposedPile.Clear();
            RiggedCardIds.Clear();

            if (CurrentMode != CardGameMode.TexasHoldEm)
            {
                for (int i = 0; i < MaxSeats; i++)
                {
                    if (Seats[i].Type == SeatType.House)
                        Seats[i] = new SeatInfo();
                }
            }

            string modeLabel = newMode switch
            {
                CardGameMode.TexasHoldEm => CardGameLang.Get("UI.TabHoldEm"),
                CardGameMode.Uno => CardGameLang.Get("UI.TabUno"),
                _ => CardGameLang.Get("UI.TabPresident")
            };

            StatusMessage = hadStocks
                ? CardGameLang.Get("Messages.ModeSwitchedRefunded", modeLabel)
                : CardGameLang.Get("Messages.ModeSwitched", modeLabel);
        }
        public void RequestAddBuyIn()
        {
            if (GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            int mySeat = GetLocalControlledSeat();
            if (mySeat == -1 || BetAmountCopper <= 0)
                return;

            if (!Main.LocalPlayer.CanAfford(BetAmountCopper))
            {
                StatusMessage = CardGameLang.Get("Messages.PlayerCannotAffordBuyIn", Main.LocalPlayer.name, CardGameLang.FormatCoins(BetAmountCopper));
                return;
            }

            Main.LocalPlayer.BuyItem(BetAmountCopper);
            SoundEngine.PlaySound(SoundID.Coins);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestAddBuyIn);
                packet.Write(BetAmountCopper);
                packet.Send();
                return;
            }

            ServerAddBuyIn(Main.myPlayer, BetAmountCopper);
            SyncStateToClients();
        }

        private void ServerAddBuyIn(int playerWhoAmI, int amountCopper)
        {
            if (GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == playerWhoAmI)
                {
                    Seats[i].StackCopper += amountCopper;
                    StatusMessage = CardGameLang.Get("Messages.BoughtInMore",
                        Seats[i].GetDisplayName(i),
                        CardGameLang.FormatCoins(amountCopper),
                        CardGameLang.FormatCoins(Seats[i].StackCopper));
                    break;
                }
            }
        }

        public static bool TryGetSellableLocalItem(out Item targetItem, out int sellPriceCopper)
        {
            targetItem = null;
            sellPriceCopper = 0;
            Player plr = Main.LocalPlayer;
            if (plr == null || !plr.active)
                return false;

            Item candidate = (Main.mouseItem != null && !Main.mouseItem.IsAir) ? Main.mouseItem : plr.HeldItem;
            if (candidate == null || candidate.IsAir || candidate.IsACoin || candidate.value <= 0 || candidate.stack <= 0)
                return false;

            if (candidate.type == ModContent.ItemType<Content.Items.Other.CardDeckItem>())
                return false;

            long rawVal = (long)candidate.value * candidate.stack / 5;
            sellPriceCopper = (int)Math.Clamp(rawVal, 1, 50000000);
            targetItem = candidate;
            return true;
        }

        public void RequestSellHeldItem()
        {
            if (GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            if (!TryGetSellableLocalItem(out Item itemToSell, out int sellValue))
            {
                StatusMessage = CardGameLang.Get("Messages.NothingToSell");
                return;
            }

            string soldItemName = $"{itemToSell.Name} ({itemToSell.stack})";
            itemToSell.TurnToAir();
            SoundEngine.PlaySound(SoundID.Coins);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestSellHeldItem);
                packet.Write(sellValue);
                packet.Write(soldItemName);
                packet.Send();
                return;
            }

            ServerAddSoldItemFunds(Main.myPlayer, sellValue, soldItemName);
            SyncStateToClients();
        }

        private void ServerAddSoldItemFunds(int playerWhoAmI, int sellValue, string itemName)
        {
            if (GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == playerWhoAmI)
                {
                    Seats[i].StackCopper += sellValue;
                    StatusMessage = CardGameLang.Get("Messages.SoldItemForFunds",
                        Seats[i].GetDisplayName(i),
                        itemName,
                        CardGameLang.FormatCoins(sellValue));
                    return;
                }
            }
        }

        public void RequestRetreatCashOut()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestRetreatCashOut);
                packet.Send();
                ModContent.GetInstance<CardGameUISystem>().CloseUI();
                return;
            }

            ServerRetreatCashOut(Main.myPlayer);
            ModContent.GetInstance<CardGameUISystem>().CloseUI();
        }

        private void ServerRetreatCashOut(int playerWhoAmI)
        {
            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == playerWhoAmI)
                {
                    int cashOut = Seats[i].StackCopper;
                    Seats[i].StackCopper = 0;
                    if (cashOut > 0)
                        SendCoinsToHumanSeat(i, cashOut);

                    string pName = Seats[i].GetDisplayName(i);
                    if (playerWhoAmI == HostPlayerWhoAmI)
                    {
                        ServerEndTableBecauseHostLeft();
                        return;
                    }

                    ServerForfeitSeat(i, pName, leftTableCompletely: true);
                    StatusMessage = CardGameLang.Get("Messages.PlayerRetreated", pName, CardGameLang.FormatCoins(cashOut));
                    SyncStateToClients();
                    return;
                }
            }
        }

        public void CycleBetAmount(int direction)
        {
            if (!IsLocalPlayerHost() || GameInProgress)
                return;

            int idx = Array.IndexOf(BetStepsCopper, BetAmountCopper);
            if (idx < 0) idx = 0;
            idx = (idx + direction + BetStepsCopper.Length) % BetStepsCopper.Length;
            int nextBet = Math.Clamp(BetStepsCopper[idx], MinBetCopper, MaxBetCopper);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestSetBetAmount);
                packet.Write(nextBet);
                packet.Send();
                return;
            }

            ServerSetBetAmount(nextBet);
            SyncStateToClients();
        }

        private void ServerSetBetAmount(int newBetCopper)
        {
            if (GameInProgress)
                return;

            BetAmountCopper = Math.Clamp(newBetCopper, MinBetCopper, MaxBetCopper);
            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Bot)
                    Seats[i].StackCopper = BetAmountCopper;
            }
            StatusMessage = CardGameLang.Get("Messages.BetUpdated", CardGameLang.FormatCoins(BetAmountCopper));
        }

        public void BroadcastPendingInvitesWithCurrentBet()
        {
            if (Main.netMode != NetmodeID.Server || !TableIsOpen || HostPlayerWhoAmI < 0)
                return;

            string hostName = Main.player[HostPlayerWhoAmI].name;
            Vector2 hostPos = Main.player[HostPlayerWhoAmI].Center;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (i == HostPlayerWhoAmI || !Main.player[i].active || IsPlayerSeatedAtTable(i))
                    continue;

                if (Vector2.Distance(Main.player[i].Center, hostPos) <= InviteRadiusPixels)
                {
                    ModPacket invitePacket = Mod.GetPacket();
                    invitePacket.Write((byte)CardPacketType.SendInvitePrompt);
                    invitePacket.Write(hostName);
                    invitePacket.Write(BetAmountCopper);
                    invitePacket.Send(toClient: i);
                }
            }
        }

        public void RequestHostGameFromThrow(Vector2 throwWorldPos)
        {
            if (GameInProgress)
                return;

            HostPlayerWhoAmI = Main.myPlayer;
            TableIsOpen = true;
            HasPendingInvite = false;
            Seats[0].Type = SeatType.Human;
            Seats[0].PlayerWhoAmI = Main.myPlayer;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestHostFromThrow);
                packet.WriteVector2(throwWorldPos);
                packet.Send();
                return;
            }

            ServerHostGameFromThrow(Main.myPlayer, throwWorldPos);
        }

        private void ServerHostGameFromThrow(int hostWhoAmI, Vector2 throwWorldPos)
        {
            if (GameInProgress || (IsPlayerSeatedAtTable(hostWhoAmI) && HostPlayerWhoAmI != hostWhoAmI && TableIsOpen))
                return;

            for (int i = 0; i < MaxSeats; i++)
                Seats[i] = new SeatInfo();

            HostPlayerWhoAmI = hostWhoAmI;
            TableIsOpen = true;
            Seats[0].Type = SeatType.Human;
            Seats[0].PlayerWhoAmI = hostWhoAmI;
            Seats[0].LastKnownName = Main.player[hostWhoAmI].name;

            GameInProgress = false;
            TotalPotCopper = 0;
            PotDistributed = false;
            CenterPile.Clear();
            DisposedPile.Clear();
            LastPlaySeat = -1;

            string hostName = Main.player[hostWhoAmI].name;
            int invitedCount = 0;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (i == hostWhoAmI || !Main.player[i].active || IsPlayerSeatedAtTable(i))
                    continue;

                if (Vector2.Distance(Main.player[i].Center, throwWorldPos) <= InviteRadiusPixels)
                {
                    invitedCount++;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket invitePacket = Mod.GetPacket();
                        invitePacket.Write((byte)CardPacketType.SendInvitePrompt);
                        invitePacket.Write(hostName);
                        invitePacket.Write(BetAmountCopper);
                        invitePacket.Send(toClient: i);
                    }
                }
            }

            StatusMessage = invitedCount > 0
                ? CardGameLang.Get("Messages.HostedWithInvites", hostName, invitedCount)
                : CardGameLang.Get("Messages.HostedNoInvites", hostName);

            SyncStateToClients();
        }

        public bool TryRespondToInvite(bool accepted, out string error)
        {
            error = "";
            int requiredBuyIn = PendingInviteBetCopper > 0 ? PendingInviteBetCopper : BetAmountCopper;
            if (accepted)
            {
                if (!Main.LocalPlayer.CanAfford(requiredBuyIn))
                {
                    error = CardGameLang.Get("Messages.CannotAffordInviteBuyIn", CardGameLang.FormatCoins(requiredBuyIn));
                    Main.NewText(error, Color.OrangeRed);
                    return false;
                }
                Main.LocalPlayer.BuyItem(requiredBuyIn);
            }

            HasPendingInvite = false;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RespondInvitePrompt);
                packet.Write(accepted);
                packet.Send();
            }

            if (accepted)
                ModContent.GetInstance<CardGameUISystem>().OpenTableUI();
            else
                ModContent.GetInstance<CardGameUISystem>().CloseUI();

            return true;
        }

        private void ServerHandleInviteResponse(int clientWhoAmI, bool accepted)
        {
            if (!TableIsOpen)
                return;

            Player clientPlayer = Main.player[clientWhoAmI];
            string playerName = clientPlayer.name;
            if (!accepted)
            {
                StatusMessage = CardGameLang.Get("Messages.PlayerDeclined", playerName);
                SyncStateToClients();
                return;
            }

            if (GameInProgress || IsPlayerSeatedAtTable(clientWhoAmI))
                return;

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Closed)
                {
                    Seats[i].Type = SeatType.Human;
                    Seats[i].PlayerWhoAmI = clientWhoAmI;
                    Seats[i].LastKnownName = playerName;
                    Seats[i].StackCopper = BetAmountCopper;
                    Seats[i].HasForfeited = false;
                    StatusMessage = CardGameLang.Get("Messages.PlayerJoined", playerName, i + 1, CardGameLang.FormatCoins(BetAmountCopper));
                    SyncStateToClients();
                    return;
                }
            }

            StatusMessage = CardGameLang.Get("Messages.TableFull", playerName);
            SyncStateToClients();
        }

        public void RequestForfeitRound()
        {
            if (!GameInProgress)
                return;

            if (CurrentMode == CardGameMode.TexasHoldEm)
            {
                RequestHoldEmAction(HoldEmActionType.Fold);
                return;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestForfeitRound);
                packet.Send();
                return;
            }

            ServerForfeitPlayerByWhoAmI(Main.myPlayer, leftTableCompletely: false);
            SyncStateToClients();
        }

        public void RequestLeaveTable()
        {
            HasPendingInvite = false;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestLeaveTable);
                packet.Send();
                return;
            }

            ServerHandlePlayerExit(Main.myPlayer);
        }

        private void ServerHandlePlayerExit(int leavingWhoAmI)
        {
            if (leavingWhoAmI == HostPlayerWhoAmI)
            {
                ServerEndTableBecauseHostLeft();
                return;
            }

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == leavingWhoAmI && Seats[i].StackCopper > 0)
                {
                    SendCoinsToHumanSeat(i, Seats[i].StackCopper);
                    Seats[i].StackCopper = 0;
                }
            }

            ServerForfeitPlayerByWhoAmI(leavingWhoAmI, leftTableCompletely: true);
            SyncStateToClients();
        }

        private void ServerEndTableBecauseHostLeft()
        {
            bool closedMidGame = GameInProgress && !PotDistributed;

            for (int i = 0; i < MaxSeats; i++)
            {
                SeatInfo s = Seats[i];
                if (s.Type == SeatType.Human && s.PlayerWhoAmI >= 0)
                {
                    int refund = s.StackCopper;
                    if (closedMidGame && s.PlayerWhoAmI != HostPlayerWhoAmI)
                        refund += (CurrentMode == CardGameMode.President || CurrentMode == CardGameMode.Uno)
                            ? BetAmountCopper
                            : s.CurrentStreetBetCopper;

                    if (refund > 0)
                        SendCoinsToHumanSeat(i, refund);
                    s.StackCopper = 0;
                }
            }

            PotDistributed = true;
            ResetTable();
            StatusMessage = closedMidGame
                ? CardGameLang.Get("Messages.HostLeftMidGameRefunded")
                : CardGameLang.Get("Messages.HostLeftEnded");

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.CloseTableForAll);
                packet.Write(StatusMessage);
                packet.Send();
            }
        }

        private void ServerForfeitPlayerByWhoAmI(int playerWhoAmI, bool leftTableCompletely)
        {
            string playerName = (playerWhoAmI >= 0 && playerWhoAmI < Main.maxPlayers && Main.player[playerWhoAmI].active)
                ? Main.player[playerWhoAmI].name
                : CardGameLang.Get("Seats.Player", 1);

            for (int i = 0; i < MaxSeats; i++)
            {
                if (Seats[i].Type == SeatType.Human && Seats[i].PlayerWhoAmI == playerWhoAmI)
                    ServerForfeitSeat(i, playerName, leftTableCompletely);
            }
        }

        private void ServerForfeitSeat(int seatIdx, string playerName, bool leftTableCompletely)
        {
            SeatInfo seat = Seats[seatIdx];
            seat.LastKnownName = playerName;

            if (!GameInProgress)
            {
                if (leftTableCompletely && seatIdx != 0)
                {
                    seat.Type = SeatType.Closed;
                    seat.PlayerWhoAmI = -1;
                    seat.LastKnownName = "";
                    seat.Hand.Clear();
                    StatusMessage = CardGameLang.Get("Messages.PlayerLeftLobby", playerName, seatIdx + 1);
                }
                return;
            }

            if (!seat.HasForfeited)
            {
                DisposedPile.AddRange(seat.Hand);
                DisposedPile.Sort();
                seat.HasForfeited = true;
                seat.HasPassed = true;
                seat.CalledUno = false;
                seat.FinishRank = 4;
                seat.Hand.Clear();
            }

            if (leftTableCompletely && seatIdx != 0)
            {
                seat.Type = SeatType.Closed;
                seat.PlayerWhoAmI = -1;
            }

            StatusMessage = leftTableCompletely
                ? CardGameLang.Get("Messages.PlayerLeftLost", playerName)
                : CardGameLang.Get("Messages.PlayerForfeitedLost", playerName);

            var remainingSeats = Enumerable.Range(0, MaxSeats).Where(idx => Seats[idx].IsActiveInRound).ToList();
            if (remainingSeats.Count <= 1)
            {
                if (CurrentMode == CardGameMode.TexasHoldEm)
                {
                    if (remainingSeats.Count == 1)
                        ResolveHoldEmFoldWin(remainingSeats[0]);
                    else
                        GameInProgress = false;
                    return;
                }

                if (remainingSeats.Count == 1)
                {
                    int winnerIdx = remainingSeats[0];
                    Seats[winnerIdx].FinishRank = NextFinishRank;
                    StatusMessage += CardGameLang.Get("Messages.WinnerAnnouncement", Seats[winnerIdx].GetDisplayName(winnerIdx), Seats[winnerIdx].GetTitleBadge(CurrentMode));
                }

                MoveCenterPileToDisposed();
                GameInProgress = false;
                StatusMessage += DistributeRoundPot();
                return;
            }

            if (CurrentTurnSeat == seatIdx)
            {
                if (CurrentMode == CardGameMode.TexasHoldEm)
                    AdvanceHoldEmTurn();
                else if (CurrentMode == CardGameMode.Uno)
                {
                    UnoHasDrawnThisTurn = false;
                    CurrentTurnSeat = FindNextUnoSeat(seatIdx, 1);
                }
                else
                    AdvanceToNextTurn();
            }
        }

        public void SetupBotTestMode(int hostPlayerIndex)
        {
            if (!IsLocalPlayerHost() || GameInProgress)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestFillBots);
                packet.Send();
                return;
            }

            ServerSetupBotTestMode(hostPlayerIndex);
            SyncStateToClients();
        }

        private void ServerSetupBotTestMode(int hostPlayerIndex)
        {
            if (GameInProgress)
                return;

            HostPlayerWhoAmI = hostPlayerIndex;
            TableIsOpen = true;
            int existingHostStack = Seats[0].Type == SeatType.Human ? Seats[0].StackCopper : 0;
            Seats[0] = new SeatInfo
            {
                Type = SeatType.Human,
                PlayerWhoAmI = hostPlayerIndex,
                LastKnownName = Main.player[hostPlayerIndex].name,
                StackCopper = existingHostStack
            };
            for (int i = 1; i < MaxSeats; i++)
                Seats[i] = new SeatInfo { Type = SeatType.Bot, PlayerWhoAmI = -1, StackCopper = BetAmountCopper };

            CenterPile.Clear();
            DisposedPile.Clear();
            StatusMessage = CardGameLang.Get("Messages.BotModeEnabled", CardGameLang.FormatCoins(BetAmountCopper));
        }

        public void RequestSetupHouseMode()
        {
            if (!IsLocalPlayerHost() || GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestSetupHouseMode);
                packet.Send();
                return;
            }

            ServerSetupHouseMode(Main.myPlayer);
            SyncStateToClients();
        }

        private void ServerSetupHouseMode(int hostPlayerIndex)
        {
            if (GameInProgress || CurrentMode != CardGameMode.TexasHoldEm)
                return;

            HostPlayerWhoAmI = hostPlayerIndex;
            TableIsOpen = true;
            int existingHostStack = Seats[0].Type == SeatType.Human ? Seats[0].StackCopper : 0;
            Seats[0] = new SeatInfo
            {
                Type = SeatType.Human,
                PlayerWhoAmI = hostPlayerIndex,
                LastKnownName = Main.player[hostPlayerIndex].name,
                StackCopper = existingHostStack
            };
            Seats[1] = new SeatInfo
            {
                Type = SeatType.House,
                PlayerWhoAmI = -1,
                StackCopper = HouseBankrollCopper
            };
            for (int i = 2; i < MaxSeats; i++)
                Seats[i] = new SeatInfo();

            CenterPile.Clear();
            DisposedPile.Clear();
            StatusMessage = CardGameLang.Get("Messages.HouseModeEnabled");
        }

        public void RequestToggleHardMode()
        {
            if (!IsLocalPlayerHost() || GameInProgress)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestToggleHardMode);
                packet.Send();
                return;
            }

            HardMode = !HardMode;
            StatusMessage = CardGameLang.Get(HardMode ? "Messages.HardModeEnabled" : "Messages.HardModeDisabled");
            SyncStateToClients();
        }

        public void CycleSeatConfiguration(int seatIndex)
        {
            if (seatIndex <= 0 || seatIndex >= MaxSeats || !IsLocalPlayerHost() || GameInProgress)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)CardPacketType.RequestConfigureSeat);
                packet.Write((byte)seatIndex);
                packet.Send();
                return;
            }

            ServerCycleSeat(seatIndex);
            SyncStateToClients();
        }

        private void ServerCycleSeat(int seatIndex)
        {
            if (GameInProgress || seatIndex <= 0)
                return;

            SeatInfo seat = Seats[seatIndex];
            var options = new List<(SeatType type, int whoAmI)>();

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Main.player[i].active && i != HostPlayerWhoAmI)
                    options.Add((SeatType.Human, i));
            }
            options.Add((SeatType.Bot, -1));
            if (CurrentMode == CardGameMode.TexasHoldEm)
                options.Add((SeatType.House, -1));
            options.Add((SeatType.Closed, -1));

            int currentIndex = options.FindIndex(o => o.type == seat.Type && o.whoAmI == seat.PlayerWhoAmI);
            int nextIndex = (currentIndex + 1) % options.Count;

            seat.Type = options[nextIndex].type;
            seat.PlayerWhoAmI = options[nextIndex].whoAmI;
            seat.StackCopper = seat.Type == SeatType.Bot
                ? BetAmountCopper
                : (seat.Type == SeatType.House ? HouseBankrollCopper : 0);
            seat.LastKnownName = (seat.Type == SeatType.Human && seat.PlayerWhoAmI >= 0)
                ? Main.player[seat.PlayerWhoAmI].name
                : "";
            seat.HasForfeited = false;
        }
    }
}