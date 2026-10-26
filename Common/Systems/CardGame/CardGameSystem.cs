using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace HendecamMod.Common.Systems.CardGame
{
    public static class CardGameLang
    {
        public static string Get(string suffix, params object[] args)
        {
            string key = $"Mods.HendecamMod.CardGame.{suffix}";
            return args == null || args.Length == 0
                ? Language.GetTextValue(key)
                : Language.GetTextValue(key, args);
        }

        public static string FormatCoins(int copperValue)
        {
            if (copperValue <= 0)
                return Get("Betting.NoBet");

            int plat = copperValue / 1000000;
            int gold = (copperValue / 10000) % 100;
            int silver = (copperValue / 100) % 100;
            int copper = copperValue % 100;

            List<string> parts = new List<string>();
            if (plat > 0) parts.Add($"{plat} Platinum");
            if (gold > 0) parts.Add($"{gold} Gold");
            if (silver > 0) parts.Add($"{silver} Silver");
            if (copper > 0) parts.Add($"{copper} Copper");

            return string.Join(" ", parts);
        }
    }

    public enum CardGameMode : byte
    {
        President = 0,
        TexasHoldEm = 1,
        Uno = 2
    }

    public enum HoldEmStage : byte
    {
        PreFlop = 0,
        Flop = 1,
        Turn = 2,
        River = 3,
        Showdown = 4
    }

    public enum HoldEmActionType : byte
    {
        CheckOrCall = 0,
        Raise = 1,
        AllIn = 2,
        Fold = 3
    }

    public enum CardSuit : byte
    {
        Spade = 0,
        Club = 1,
        Diamond = 2,
        Heart = 3
    }

    public enum CardRank : byte
    {
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
        Two = 15
    }

    public enum UnoColor : byte
    {
        Red = 0,
        Yellow = 1,
        Green = 2,
        Blue = 3,
        Wild = 4
    }

    public enum UnoValue : byte
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Skip = 10,
        Reverse = 11,
        DrawTwo = 12,
        Wild = 13,
        WildDrawFour = 14
    }

    public enum SeatType : byte
    {
        Closed = 0,
        Human = 1,
        Bot = 2,
        House = 3
    }

    public enum CardHandType : byte
    {
        Invalid = 0,
        Single = 1,
        Pair = 2,
        TwoPair = 3,
        ThreePair = 4,
        ThreeOfAKind = 5,
        Straight = 6,
        FullHouse = 7,
        FourOfAKind = 8
    }

    public enum HoldEmHandRank : byte
    {
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOfAKind = 4,
        Straight = 5,
        Flush = 6,
        FullHouse = 7,
        FourOfAKind = 8,
        StraightFlush = 9
    }

    public readonly struct PlayingCard : IComparable<PlayingCard>, IEquatable<PlayingCard>
    {
        public readonly byte Id;

        public CardRank Rank => (CardRank)(3 + ((Id % 52) / 4));
        public CardSuit Suit => (CardSuit)((Id % 52) % 4);
        public bool IsRed => Suit == CardSuit.Diamond || Suit == CardSuit.Heart;
        public int PokerRank => Rank == CardRank.Two ? 2 : (int)Rank;
        public UnoValue UnoVal => (UnoValue)((Id % 60) % 15);
        public UnoColor UnoCol => UnoVal >= UnoValue.Wild ? UnoColor.Wild : (UnoColor)((Id % 60) / 15);
        public bool IsUnoWild => UnoVal == UnoValue.Wild || UnoVal == UnoValue.WildDrawFour;

        public PlayingCard(byte id) => Id = id;
        public PlayingCard(CardRank rank, CardSuit suit) => Id = (byte)(((int)rank - 3) * 4 + (int)suit);

        public static PlayingCard CreateUno(UnoColor color, UnoValue value, int copyIndex = 0)
        {
            int colorSlot = color == UnoColor.Wild ? (copyIndex % 4) : (int)color;
            int copyOffset = (color == UnoColor.Wild ? (copyIndex / 4) : copyIndex) * 60;
            return new PlayingCard((byte)Math.Clamp(copyOffset + colorSlot * 15 + (int)value, 0, 239));
        }

        public string GetRankName() => CardGameLang.Get($"Ranks.{Rank}");
        public string GetSuitName() => CardGameLang.Get($"Suits.{Suit}");
        public string GetFullName() => CardGameLang.Get("CardNameFormat", GetRankName(), GetSuitName());

        public string GetUnoColorName() => CardGameLang.Get($"UnoColors.{UnoCol}");
        public string GetUnoValueName() => CardGameLang.Get($"UnoValues.{UnoVal}");
        public string GetUnoFullName() => IsUnoWild
            ? GetUnoValueName()
            : CardGameLang.Get("UnoCardNameFormat", GetUnoColorName(), GetUnoValueName());

        public Color GetSuitColor() => IsRed
            ? new Color(220, 45, 45)
            : new Color(35, 35, 45);

        public static Color GetUnoColorTint(UnoColor col) => col switch
        {
            UnoColor.Red => new Color(235, 62, 62),
            UnoColor.Yellow => new Color(250, 200, 45),
            UnoColor.Green => new Color(65, 210, 80),
            UnoColor.Blue => new Color(65, 145, 250),
            _ => Color.White
        };

        public Color GetUnoTintColor() => IsUnoWild ? Color.White : GetUnoColorTint(UnoCol);

        public Rectangle GetSheetSourceRect(int cardWidth = 56, int cardHeight = 80)
        {
            int col = (int)Rank - 3;
            int row = (int)Suit;
            return new Rectangle(col * cardWidth, row * cardHeight, cardWidth, cardHeight);
        }

        public Rectangle GetUnoSheetSourceRect(int frameWidth, int frameHeight)
        {
            int frameIdx = Math.Clamp((int)UnoVal + 1, 1, 15);
            return new Rectangle(0, frameIdx * frameHeight, frameWidth, frameHeight);
        }

        public int CompareTo(PlayingCard other) => Id.CompareTo(other.Id);
        public bool Equals(PlayingCard other) => Id == other.Id;
        public override bool Equals(object obj) => obj is PlayingCard other && Equals(other);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => GetFullName();
    }
}