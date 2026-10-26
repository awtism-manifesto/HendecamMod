using System.Collections.Generic;
using HendecamMod.Common.Systems.CardGame;
using Terraria;

namespace HendecamMod.Networking.Packets
{
    public enum CardPacketType : byte
    {
        SyncFullState = 1,
        RequestConfigureSeat = 2,
        RequestFillBots = 3,
        RequestStartGame = 4,
        RequestPlayCards = 5,
        RequestPassTurn = 6,
        RequestHostFromThrow = 7,
        SendInvitePrompt = 8,
        RespondInvitePrompt = 9,
        RequestForfeitRound = 10,
        RequestLeaveTable = 11,
        CloseTableForAll = 12,
        RequestToggleHardMode = 13,
        RequestSetBetAmount = 14,
        GrantCoinPayout = 15,
        RequestSetGameMode = 16,
        RequestSetupHouseMode = 17,
        RequestAddBuyIn = 18,
        RequestSellHeldItem = 19,
        RequestRetreatCashOut = 20,
        RequestHoldEmAction = 21,
        RequestPlayUnoCard = 22,
        RequestCallUno = 23
    }

    public class SeatInfo
    {
        public SeatType Type = SeatType.Closed;
        public int PlayerWhoAmI = -1;
        public List<PlayingCard> Hand = new List<PlayingCard>();
        public bool HasPassed;
        public bool HasForfeited;
        public bool IsAllIn;
        public bool HasActedThisStreet;
        public bool CalledUno;
        public int StackCopper;
        public int CurrentStreetBetCopper;
        public int FinishRank;
        public string LastKnownName = "";

        public bool IsActiveInRound => Type != SeatType.Closed && !HasForfeited && Hand.Count > 0;
        public bool CanActInHoldEm => Type != SeatType.Closed && !HasForfeited && !IsAllIn && Hand.Count > 0;

        public string GetDisplayName(int seatIndex)
        {
            if (Type == SeatType.House)
                return CardGameLang.Get("Seats.House");
            if (Type == SeatType.Bot)
                return CardGameLang.Get("Seats.Bot", seatIndex + 1);
            if (PlayerWhoAmI >= 0 && PlayerWhoAmI < Main.maxPlayers && Main.player[PlayerWhoAmI].active)
            {
                LastKnownName = Main.player[PlayerWhoAmI].name;
                return LastKnownName;
            }
            if (!string.IsNullOrEmpty(LastKnownName))
                return LastKnownName;
            if (Type == SeatType.Closed)
                return CardGameLang.Get("Seats.Empty", seatIndex + 1);
            return CardGameLang.Get("Seats.Player", seatIndex + 1);
        }

        public string GetTitleBadge(CardGameMode mode)
        {
            if (HasForfeited)
                return CardGameLang.Get(mode == CardGameMode.TexasHoldEm ? "Titles.Folded" : "Titles.Lost");
            if (IsAllIn && mode == CardGameMode.TexasHoldEm && FinishRank == 0)
                return CardGameLang.Get("Titles.AllIn");
            if (mode == CardGameMode.Uno && CalledUno && Hand.Count == 1 && FinishRank == 0)
                return CardGameLang.Get("Titles.UnoCalled");
            if (Type == SeatType.House && FinishRank == 0)
                return CardGameLang.Get("Titles.House");

            if (mode == CardGameMode.TexasHoldEm)
            {
                return FinishRank switch
                {
                    1 => CardGameLang.Get("Titles.Winner"),
                    4 => CardGameLang.Get("Titles.Lost"),
                    _ => ""
                };
            }

            return FinishRank switch
            {
                1 => CardGameLang.Get("Titles.President"),
                2 => CardGameLang.Get("Titles.VicePres"),
                3 => CardGameLang.Get("Titles.ViceScum"),
                4 => CardGameLang.Get("Titles.Scum"),
                _ => ""
            };
        }
    }
}