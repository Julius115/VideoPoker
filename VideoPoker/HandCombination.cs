using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    public enum HandCombinationTypes
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

    class HandCombination
    {
        public static HandCombinationTypes GetHandCombination(List<Card> playCards)
        {
            bool isFlush = true;
            bool isStraight = true;
            bool isThreeOfAKind = false;
            bool isFourOfAKind = false;

            CardSuits suit = playCards[0].Suit;

            foreach(Card card in playCards)
            {
                if(suit != card.Suit)
                {
                    isFlush = false;
                    break;
                }
            }

            List<CardRanks> ranks = new List<CardRanks>();

            foreach(Card card in playCards)
            {
                ranks.Add(card.Rank);
            }

            List<Card> orderedCards = playCards.OrderBy(i => i.Rank).ToList();

            CardRanks cardRank = orderedCards[0].Rank;

            for (int i = 1; i < 5; i++)
            {
                if ((cardRank+1) != (orderedCards[i].Rank))
                {
                    isStraight = false;
                    break;
                }
                cardRank = orderedCards[i].Rank;
            }

            List<Card> pairs = new List<Card>();
            foreach (Card card in playCards)
            {
                int tempAmountOfDuplicates = playCards.Where(p => p.Rank == card.Rank).ToList().Count;
                
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
                    if(!pairs.Any(p => p.Rank == card.Rank)){
                        pairs.Add(card);
                    }
                }
                
            }

            if (isStraight)
            {
                if (isFlush)
                {
                    if (orderedCards[0].Rank == CardRanks.Ten)
                    {
                        return HandCombinationTypes.RoyalFlush;
                    }
                    else
                    {
                        return HandCombinationTypes.StraightFlush;
                    }
                }
                else
                {
                    return HandCombinationTypes.Straight;
                }

            }

            if (isFlush)
            {
                return HandCombinationTypes.Flush;
            }

            if (isFourOfAKind)
            {
                return HandCombinationTypes.FourOfAKind;
            }

            if (isThreeOfAKind)
            {
                if(pairs.Count == 2)
                {
                    return HandCombinationTypes.FullHouse;
                }

                return HandCombinationTypes.ThreeOfAKind;
            }

            if (pairs.Count == 2)
            {
                return HandCombinationTypes.TwoPair;
            }

            if(pairs.Count == 1 && pairs[0].Rank >= CardRanks.Jack)
            {
                return HandCombinationTypes.JacksOrBetter;
            }

            return HandCombinationTypes.AllOther;
        }
    }
}
