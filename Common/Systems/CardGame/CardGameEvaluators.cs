using System;
using System.Collections.Generic;
using System.Linq;

namespace HendecamMod.Common.Systems.CardGame
{
    public readonly struct EvaluatedHand
    {
        public readonly CardHandType Type;
        public readonly int CardCount;
        public readonly CardRank[] RankPriority;
        public readonly CardSuit TieBreakerSuit;
        public readonly byte SuitMask;
        public readonly string DisplayName;

        public CardRank PrimaryRank => (RankPriority != null && RankPriority.Length > 0) ? RankPriority[0] : 0;

        public EvaluatedHand(CardHandType type, int count, CardRank[] ranks, CardSuit suit, byte suitMask, string display)
        {
            Type = type;
            CardCount = count;
            RankPriority = ranks ?? Array.Empty<CardRank>();
            TieBreakerSuit = suit;
            SuitMask = suitMask;
            DisplayName = display;
        }
    }

    public readonly struct HoldEmEvaluatedHand : IComparable<HoldEmEvaluatedHand>
    {
        public readonly HoldEmHandRank Category;
        public readonly int Score;
        public readonly string DisplayName;

        public HoldEmEvaluatedHand(HoldEmHandRank category, int score, string displayName)
        {
            Category = category;
            Score = score;
            DisplayName = displayName;
        }

        public int CompareTo(HoldEmEvaluatedHand other) => Score.CompareTo(other.Score);
    }

    public static class HoldEmEvaluator
    {
        private static string PokerRankName(int pokerRank)
        {
            CardRank cr = pokerRank == 2 ? CardRank.Two : (CardRank)pokerRank;
            return new PlayingCard(cr, CardSuit.Spade).GetRankName();
        }

        public static HoldEmEvaluatedHand EvaluateBest(IEnumerable<PlayingCard> holeCards, IEnumerable<PlayingCard> communityCards)
        {
            List<PlayingCard> all = new List<PlayingCard>();
            if (holeCards != null) all.AddRange(holeCards);
            if (communityCards != null) all.AddRange(communityCards);

            if (all.Count < 5)
                return EvaluateFive(all);

            HoldEmEvaluatedHand best = default;
            int n = all.Count;
            List<PlayingCard> buf = new List<PlayingCard>(5);

            for (int a = 0; a < n - 4; a++)
                for (int b = a + 1; b < n - 3; b++)
                    for (int c = b + 1; c < n - 2; c++)
                        for (int d = c + 1; d < n - 1; d++)
                            for (int e = d + 1; e < n; e++)
                            {
                                buf.Clear();
                                buf.Add(all[a]);
                                buf.Add(all[b]);
                                buf.Add(all[c]);
                                buf.Add(all[d]);
                                buf.Add(all[e]);
                                HoldEmEvaluatedHand candidate = EvaluateFive(buf);
                                if (candidate.Score > best.Score)
                                    best = candidate;
                            }

            return best;
        }

        private static HoldEmEvaluatedHand EvaluateFive(List<PlayingCard> cards)
        {
            if (cards == null || cards.Count == 0)
                return new HoldEmEvaluatedHand(HoldEmHandRank.HighCard, 0, CardGameLang.Get("Hands.Invalid"));

            var sorted = cards.OrderByDescending(c => c.PokerRank).ThenByDescending(c => c.Suit).ToList();
            bool isFlush = sorted.Count == 5 && sorted.All(c => c.Suit == sorted[0].Suit);

            bool isStraight = false;
            int straightHigh = sorted[0].PokerRank;
            if (sorted.Count == 5 && sorted.Select(c => c.PokerRank).Distinct().Count() == 5)
            {
                if (sorted[0].PokerRank - sorted[4].PokerRank == 4)
                {
                    isStraight = true;
                    straightHigh = sorted[0].PokerRank;
                }
                else if (sorted[0].PokerRank == 14 && sorted[1].PokerRank == 5 && sorted[2].PokerRank == 4 && sorted[3].PokerRank == 3 && sorted[4].PokerRank == 2)
                {
                    isStraight = true;
                    straightHigh = 5;
                }
            }

            var groups = sorted
                .GroupBy(c => c.PokerRank)
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.Key)
                .ToList();

            int[] tieRanks = groups.SelectMany(g => g.Select(c => c.PokerRank)).ToArray();

            if (isFlush && isStraight)
            {
                int sc = PackScore(HoldEmHandRank.StraightFlush, straightHigh, 0, 0, 0, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.StraightFlush, sc,
                    CardGameLang.Get("HoldEmHands.StraightFlush", sorted[0].GetSuitName(), PokerRankName(straightHigh)));
            }

            if (groups[0].Count() == 4)
            {
                int quad = groups[0].Key;
                int kicker = groups.Count > 1 ? groups[1].Key : 0;
                int sc = PackScore(HoldEmHandRank.FourOfAKind, quad, kicker, 0, 0, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.FourOfAKind, sc,
                    CardGameLang.Get("HoldEmHands.FourOfAKind", PokerRankName(quad)));
            }

            if (groups[0].Count() == 3 && groups.Count > 1 && groups[1].Count() >= 2)
            {
                int trip = groups[0].Key;
                int pair = groups[1].Key;
                int sc = PackScore(HoldEmHandRank.FullHouse, trip, pair, 0, 0, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.FullHouse, sc,
                    CardGameLang.Get("HoldEmHands.FullHouse", PokerRankName(trip), PokerRankName(pair)));
            }

            if (isFlush)
            {
                int sc = PackScore(HoldEmHandRank.Flush, GetAt(tieRanks, 0), GetAt(tieRanks, 1), GetAt(tieRanks, 2), GetAt(tieRanks, 3), GetAt(tieRanks, 4));
                return new HoldEmEvaluatedHand(HoldEmHandRank.Flush, sc,
                    CardGameLang.Get("HoldEmHands.Flush", sorted[0].GetSuitName(), PokerRankName(sorted[0].PokerRank)));
            }

            if (isStraight)
            {
                int sc = PackScore(HoldEmHandRank.Straight, straightHigh, 0, 0, 0, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.Straight, sc,
                    CardGameLang.Get("HoldEmHands.Straight", PokerRankName(straightHigh)));
            }

            if (groups[0].Count() == 3)
            {
                int trip = groups[0].Key;
                int k1 = groups.Count > 1 ? groups[1].Key : 0;
                int k2 = groups.Count > 2 ? groups[2].Key : 0;
                int sc = PackScore(HoldEmHandRank.ThreeOfAKind, trip, k1, k2, 0, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.ThreeOfAKind, sc,
                    CardGameLang.Get("HoldEmHands.ThreeOfAKind", PokerRankName(trip)));
            }

            if (groups[0].Count() == 2 && groups.Count > 1 && groups[1].Count() == 2)
            {
                int p1 = groups[0].Key;
                int p2 = groups[1].Key;
                int k1 = groups.Count > 2 ? groups[2].Key : 0;
                int sc = PackScore(HoldEmHandRank.TwoPair, p1, p2, k1, 0, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.TwoPair, sc,
                    CardGameLang.Get("HoldEmHands.TwoPair", PokerRankName(p1), PokerRankName(p2)));
            }

            if (groups[0].Count() == 2)
            {
                int p1 = groups[0].Key;
                int k1 = groups.Count > 1 ? groups[1].Key : 0;
                int k2 = groups.Count > 2 ? groups[2].Key : 0;
                int k3 = groups.Count > 3 ? groups[3].Key : 0;
                int sc = PackScore(HoldEmHandRank.OnePair, p1, k1, k2, k3, 0);
                return new HoldEmEvaluatedHand(HoldEmHandRank.OnePair, sc,
                    CardGameLang.Get("HoldEmHands.OnePair", PokerRankName(p1)));
            }

            int highSc = PackScore(HoldEmHandRank.HighCard, GetAt(tieRanks, 0), GetAt(tieRanks, 1), GetAt(tieRanks, 2), GetAt(tieRanks, 3), GetAt(tieRanks, 4));
            return new HoldEmEvaluatedHand(HoldEmHandRank.HighCard, highSc,
                CardGameLang.Get("HoldEmHands.HighCard", PokerRankName(sorted[0].PokerRank)));
        }

        private static int GetAt(int[] arr, int index) => index < arr.Length ? arr[index] : 0;

        private static int PackScore(HoldEmHandRank cat, int r0, int r1, int r2, int r3, int r4)
        {
            return ((int)cat << 20) | ((r0 & 0xF) << 16) | ((r1 & 0xF) << 12) | ((r2 & 0xF) << 8) | ((r3 & 0xF) << 4) | (r4 & 0xF);
        }
    }

    public static class CardHandEvaluator
    {
        private static string RankLabel(CardRank r) => new PlayingCard(r, CardSuit.Spade).GetRankName();
        private static string SuitLabel(CardSuit s) => CardGameLang.Get($"Suits.{s}");

        private static byte ComputeSuitMask(IEnumerable<PlayingCard> cards)
        {
            byte mask = 0;
            foreach (var c in cards)
                mask |= (byte)(1 << (int)c.Suit);
            return mask;
        }

        public static EvaluatedHand Evaluate(List<PlayingCard> cards, bool hardMode = false)
        {
            if (cards == null || cards.Count == 0)
                return new EvaluatedHand(CardHandType.Invalid, 0, null, 0, 0, CardGameLang.Get("Hands.Invalid"));

            var sorted = cards.OrderBy(c => c.Id).ToList();
            int count = sorted.Count;
            byte mask = ComputeSuitMask(sorted);

            if (count == 1)
                return new EvaluatedHand(CardHandType.Single, 1, new[] { sorted[0].Rank }, sorted[0].Suit, mask,
                    CardGameLang.Get("Hands.Single", sorted[0].GetFullName()));

            var groups = sorted
                .GroupBy(c => c.Rank)
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.Key)
                .ToList();

            if (groups.Count == 1 && count >= 2 && count <= 4)
            {
                if (hardMode && count == 2 && sorted[0].IsRed != sorted[1].IsRed)
                    return new EvaluatedHand(CardHandType.Invalid, count, null, 0, 0, CardGameLang.Get("Hands.Invalid"));

                CardSuit maxSuit = sorted.Max(c => c.Suit);
                CardRank rank = groups[0].Key;
                return count switch
                {
                    2 => new EvaluatedHand(CardHandType.Pair, 2, new[] { rank }, maxSuit, mask,
                        CardGameLang.Get("Hands.Pair", RankLabel(rank), SuitLabel(maxSuit))),
                    3 => new EvaluatedHand(CardHandType.ThreeOfAKind, 3, new[] { rank }, maxSuit, mask,
                        CardGameLang.Get("Hands.ThreeOfAKind", RankLabel(rank))),
                    _ => new EvaluatedHand(CardHandType.FourOfAKind, 4, new[] { rank }, maxSuit, mask,
                        CardGameLang.Get("Hands.FourOfAKind", RankLabel(rank)))
                };
            }

            if (((count == 4 && groups.Count == 2) || (!hardMode && count == 6 && groups.Count == 3)) && groups.All(g => g.Count() == 2))
            {
                if (hardMode)
                {
                    if (!groups.All(g => g.Select(c => c.IsRed).Distinct().Count() == 1))
                        return new EvaluatedHand(CardHandType.Invalid, count, null, 0, 0, CardGameLang.Get("Hands.Invalid"));
                    if (groups[0].First().IsRed != groups[1].First().IsRed)
                        return new EvaluatedHand(CardHandType.Invalid, count, null, 0, 0, CardGameLang.Get("Hands.Invalid"));
                }

                CardRank[] pairRanks = groups.Select(g => g.Key).ToArray();
                CardSuit highSuit = groups[0].Max(c => c.Suit);
                string ranksText = string.Join(" / ", pairRanks.Select(RankLabel));
                CardHandType handType = count == 4 ? CardHandType.TwoPair : CardHandType.ThreePair;
                string key = count == 4 ? "Hands.TwoPair" : "Hands.ThreePair";
                return new EvaluatedHand(handType, count, pairRanks, highSuit, mask, CardGameLang.Get(key, ranksText));
            }

            if (count == 5 && groups.Count == 2 && groups[0].Count() == 3 && groups[1].Count() == 2)
            {
                if (hardMode && groups[1].Select(c => c.IsRed).Distinct().Count() != 1)
                    return new EvaluatedHand(CardHandType.Invalid, count, null, 0, 0, CardGameLang.Get("Hands.Invalid"));

                CardRank[] ranks = { groups[0].Key, groups[1].Key };
                CardSuit highSuit = groups[0].Max(c => c.Suit);
                byte pairMask = ComputeSuitMask(groups[1]);
                return new EvaluatedHand(CardHandType.FullHouse, 5, ranks, highSuit, pairMask,
                    CardGameLang.Get("Hands.FullHouse", RankLabel(ranks[0]), RankLabel(ranks[1])));
            }

            if (count >= 3 && groups.Count == count)
            {
                if (hardMode && sorted.Any(c => c.Suit != sorted[0].Suit))
                    return new EvaluatedHand(CardHandType.Invalid, count, null, 0, 0, CardGameLang.Get("Hands.Invalid"));

                bool consecutive = true;
                for (int i = 1; i < sorted.Count; i++)
                {
                    if ((int)sorted[i].Rank != (int)sorted[i - 1].Rank + 1)
                    {
                        consecutive = false;
                        break;
                    }
                }

                if (consecutive)
                {
                    PlayingCard high = sorted[sorted.Count - 1];
                    return new EvaluatedHand(CardHandType.Straight, count, new[] { high.Rank }, high.Suit, mask,
                        CardGameLang.Get("Hands.Straight", count, sorted[0].GetFullName(), high.GetFullName()));
                }
            }

            return new EvaluatedHand(CardHandType.Invalid, count, null, 0, 0, CardGameLang.Get("Hands.Invalid"));
        }

        public static bool CanBeat(EvaluatedHand played, EvaluatedHand pile, bool hardMode, out string reason)
        {
            if (played.Type == CardHandType.Invalid)
            {
                reason = CardGameLang.Get(hardMode ? "Messages.InvalidCombinationHard" : "Messages.InvalidCombination");
                return false;
            }

            if (pile.Type == CardHandType.Invalid || pile.CardCount == 0)
            {
                reason = "";
                return true;
            }

            if (played.Type == CardHandType.FourOfAKind &&
                (pile.Type == CardHandType.Single || pile.Type == CardHandType.Pair) &&
                pile.PrimaryRank == CardRank.Two)
            {
                reason = "";
                return true;
            }

            if (played.Type == CardHandType.Straight && played.CardCount == 5 &&
                (pile.Type == CardHandType.Single || pile.Type == CardHandType.Pair) &&
                pile.PrimaryRank == CardRank.Two)
            {
                reason = "";
                return true;
            }

            if (played.Type != pile.Type || played.CardCount != pile.CardCount)
            {
                reason = CardGameLang.Get("Messages.MustMatchPile", pile.DisplayName);
                return false;
            }

            if (hardMode)
            {
                if (pile.Type == CardHandType.Single)
                {
                    bool playedIsTwo = played.PrimaryRank == CardRank.Two;
                    bool pileIsTwo = pile.PrimaryRank == CardRank.Two;
                    if (!playedIsTwo && !pileIsTwo && played.TieBreakerSuit != pile.TieBreakerSuit)
                    {
                        reason = CardGameLang.Get("Messages.HardModeMustMatchSingleSuit", SuitLabel(pile.TieBreakerSuit));
                        return false;
                    }
                }
                else if (pile.Type == CardHandType.Pair)
                {
                    bool playedIsTwos = played.PrimaryRank == CardRank.Two;
                    bool pileIsTwos = pile.PrimaryRank == CardRank.Two;
                    if (!playedIsTwos && !pileIsTwos && played.SuitMask != pile.SuitMask)
                    {
                        reason = CardGameLang.Get("Messages.HardModeMustMatchPairSuits");
                        return false;
                    }
                }
                else if (pile.Type == CardHandType.TwoPair || pile.Type == CardHandType.Straight)
                {
                    if (played.SuitMask != pile.SuitMask)
                    {
                        reason = CardGameLang.Get("Messages.HardModeMustMatchComboSuits");
                        return false;
                    }
                }
            }

            int len = Math.Min(played.RankPriority.Length, pile.RankPriority.Length);
            for (int i = 0; i < len; i++)
            {
                if (played.RankPriority[i] > pile.RankPriority[i])
                {
                    reason = "";
                    return true;
                }
                if (played.RankPriority[i] < pile.RankPriority[i])
                {
                    reason = CardGameLang.Get("Messages.MustPlayHigherRank", pile.DisplayName);
                    return false;
                }
            }

            if (played.TieBreakerSuit > pile.TieBreakerSuit)
            {
                reason = "";
                return true;
            }

            reason = CardGameLang.Get("Messages.MustPlayHigherSuit", SuitLabel(pile.TieBreakerSuit));
            return false;
        }
    }
}