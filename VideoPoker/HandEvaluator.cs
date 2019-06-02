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
        private List<Card> cards = new List<Card>();
        private List<Card> pairs;

        private bool isThreeOfAKind = false;
        private bool isFourOfAKind = false;
        private bool isFlush = false;
        private bool isStraight = false;

        public HandCombinations EvaluateHand(List<Card> cardsInput)
        {
            cards = cardsInput;
            pairs = new List<Card>();

            isThreeOfAKind = false;
            isFourOfAKind = false;

            isFlush = CheckFlush();
            isStraight = CheckStraight();
            
            CheckDuplicates();

            return ReturnHand();
        }

        private bool CheckFlush()
        {
            CardSuits suit = cards[0].Suit;

            bool isFlush = true;

            foreach (Card card in cards)
            {
                if (suit != card.Suit)
                {
                    isFlush = false;
                    break;
                }
            }
            return isFlush;
        }

        private bool CheckStraight()
        {
            bool isStraight = true; 

            CardRanks cardRank = cards[0].Rank;

            for (int i = 1; i < 5; i++)
            {
                if ((cards[i].Rank) != (cardRank + 1))
                {
                    isStraight = false;
                    break;
                }
                cardRank = cards[i].Rank;
            }
            return isStraight;
        }
        
        private void CheckDuplicates()
        {
            foreach (Card card in cards)
            {
                int tempAmountOfDuplicates = cards.Where(p => p.Rank == card.Rank).ToList().Count;

                if (tempAmountOfDuplicates == 4)
                {
                    isFourOfAKind = true;
                    break;
                }

                if (tempAmountOfDuplicates == 3)
                {
                    if (!pairs.Any(p => p.Rank == card.Rank))
                    {
                        pairs.Add(card);
                    }
                    isThreeOfAKind = true;
                }
                if (tempAmountOfDuplicates == 2)
                {
                    if (!pairs.Any(p => p.Rank == card.Rank))
                    {
                        pairs.Add(card);
                    }
                }

            }
        }

        private HandCombinations ReturnHand()
        {
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

            if (isFourOfAKind)
            {
                return HandCombinations.FourOfAKind;
            }

            if (isThreeOfAKind)
            {
                if (pairs.Count == 2)
                {
                    return HandCombinations.FullHouse;
                }

                return HandCombinations.ThreeOfAKind;
            }

            if (pairs.Count == 2)
            {
                return HandCombinations.TwoPair;
            }

            if (pairs.Count == 1 && pairs[0].Rank >= CardRanks.Jack)
            {
                return HandCombinations.JacksOrBetter;
            }

            return HandCombinations.AllOther;
        }
    }
}
