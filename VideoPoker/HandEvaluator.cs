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
        //private List<Card> pairs;

        private bool isFlush = false;
        private bool isStraight = false;

        public HandCombinations EvaluateHand(Card[] cardsInput)
        {

            cards = SortCards(cardsInput);
            
            HandCombinations handCombination = CheckStraightAndFlushCombinations();

            if (handCombination == HandCombinations.AllOther)
            {
                handCombination = CheckDuplicates();
            }

            return handCombination;
        }

        private HandCombinations CheckStraightAndFlushCombinations()
        {
            isFlush = CheckFlush();
            isStraight = CheckStraight();

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

        private Card[] SortCards(Card[] cardsInput)
        {
            return cardsInput.OrderBy(i => i.Rank).ToArray();
        }

        private bool CheckFlush()
        {
            bool isFlush = !cards.Any(p => p.Suit != cards[0].Suit);

            return isFlush;
        }

        private bool CheckStraight()
        {
            bool isStraight = true; 

            for (int i = 0; i < 4; i++)
            {
                if ((cards[i].Rank) != (cards[i+1].Rank -1))
                {
                    isStraight = false;
                    break;
                }
            }
            return isStraight;
        }
        
        private HandCombinations CheckDuplicates()
        {
            List<Card> pairs = new List<Card>();

            bool isThreeOfAKind = false;

            foreach (Card card in cards)
            {
                int amountOfDuplicates = cards.Where(p => p.Rank == card.Rank).ToList().Count;

                if (amountOfDuplicates == 4)
                {
                    return HandCombinations.FourOfAKind;
                }

                if (amountOfDuplicates == 3)
                {
                    if (!pairs.Any(p => p.Rank == card.Rank))
                    {
                        pairs.Add(card);
                    }
                    isThreeOfAKind = true;
                    if (pairs.Count == 2)
                    {
                        return HandCombinations.FullHouse;
                    }
                }
                if (amountOfDuplicates == 2)
                {
                    if (!pairs.Any(p => p.Rank == card.Rank))
                    {
                        pairs.Add(card);
                    }
                }
            }

            if (isThreeOfAKind)
            {
                return HandCombinations.ThreeOfAKind;
            }

            if(pairs.Count == 2)
            {
                return HandCombinations.TwoPair;
            }

            if (pairs.Count == 1 && pairs[0].Rank > CardRanks.Ten)
            {
                return HandCombinations.JacksOrBetter;
            }

            return HandCombinations.AllOther;
        }
    }
}
