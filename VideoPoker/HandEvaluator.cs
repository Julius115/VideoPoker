using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    public enum HandCombinations
    {
        AllOther,
        JacksOrBetter,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush = 6,
        FullHouse = 9,
        FourOfAKind = 25,
        StraightFlush = 50,
        RoyalFlush = 800
    }

    class HandEvaluator
    {
        private Card[] cards = new Card[5];

        public HandCombinations EvaluateHand(Card[] cardsInput)
        {
            // Sorts cards in ascending order
            cards = cardsInput.OrderBy(i => i.Rank).ToArray();

            HandCombinations handCombination = CheckStraightAndFlushCombinations();

            if (handCombination == HandCombinations.AllOther)
            {
                handCombination = CheckPairsCombinations();
            }

            return handCombination;
        }

        private HandCombinations CheckStraightAndFlushCombinations()
        {
            bool isFlush = CheckFlush();
            bool isStraight = CheckStraight();

            if (isStraight)
            {
                if (isFlush)
                {
                    if (cards[0].Rank == CardRanks.Ten)
                    {
                        return HandCombinations.RoyalFlush;
                    }
                    else
                    {
                        return HandCombinations.StraightFlush;
                    }
                }
                else
                {
                    return HandCombinations.Straight;
                }
            }

            if (isFlush)
            {
                return HandCombinations.Flush;
            }

            return HandCombinations.AllOther;
        }

        private bool CheckStraight()
        {
            bool isStraight = true;

            for (int i = 0; i < 4; i++)
            {
                if ((cards[i].Rank) != (cards[i + 1].Rank - 1))
                {
                    isStraight = false;
                    break;
                }
            }

            return isStraight;
        }

        private bool CheckFlush()
        {
            bool isFlush = !cards.Any(p => p.Suit != cards[0].Suit);

            return isFlush;
        }

        private HandCombinations CheckPairsCombinations()
        {
            List<Card> pairs = new List<Card>();
            var distinctRanks = cards.Select(x => x.Rank).Distinct().ToArray();

            bool isJacksOrBetter = false;
            bool isOnePair = false;
            bool isTwoPair = false;
            bool isThreeOfAKind = false;
            bool isFourOfAkind = false;

            foreach (CardRanks distinctRank in distinctRanks)
            {
                int matchesCount = cards.Where(p => p.Rank == distinctRank).ToList().Count;

                if (matchesCount == 4)
                {
                    isFourOfAkind = true;
                    break;
                }

                if (matchesCount == 3)
                {
                    isThreeOfAKind = true;
                }

                if (matchesCount == 2)
                {
                    if (!isOnePair) {
                        isOnePair = true;
                        if (distinctRank > CardRanks.Ten)
                        {
                            isJacksOrBetter = true;
                        }
                    }
                    else
                    {
                        isTwoPair = true;
                    }
                }
            }
            
            if (isFourOfAkind)
            {
                return HandCombinations.FourOfAKind;
            }

            if (isThreeOfAKind)
            {
                if (isOnePair)
                {
                    return HandCombinations.FullHouse;
                }
                return HandCombinations.ThreeOfAKind;
            }

            if (isTwoPair)
            {
                return HandCombinations.TwoPair;
            }

            if (isJacksOrBetter)
            {
                return HandCombinations.JacksOrBetter;
            }

            return HandCombinations.AllOther;
        }
    }
}