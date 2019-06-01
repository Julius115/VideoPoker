using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    public enum HandCombinationTypesEnum
    {
        AllOther,
        JacksOrBetter,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    class HandCombination
    {
        public static HandCombinationTypesEnum GetHandCombination(List<Card> playCards)
        {
            bool isFlush = true;
            bool isStraight = true;
            bool isThreeOfAKind = false;
            bool isFourOfAKind = false;

            CardSuitsEnum suit = playCards[0].Suit;

            foreach(Card card in playCards)
            {
                if(suit != card.Suit)
                {
                    isFlush = false;
                    break;
                }
            }

            List<CardRanksEnum> ranks = new List<CardRanksEnum>();

            foreach(Card card in playCards)
            {
                ranks.Add(card.Rank);
            }

            List<Card> orderedCards = playCards.OrderBy(i => i.Rank).ToList();

            CardRanksEnum cardRank = orderedCards[0].Rank;

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
                    if (orderedCards[0].Rank == CardRanksEnum.Ten)
                    {
                        return HandCombinationTypesEnum.RoyalFlush;
                    }
                    else
                    {
                        return HandCombinationTypesEnum.StraightFlush;
                    }
                }
                else
                {
                    return HandCombinationTypesEnum.Straight;
                }

            }

            if (isFlush)
            {
                return HandCombinationTypesEnum.Flush;
            }

            if (isFourOfAKind)
            {
                return HandCombinationTypesEnum.FourOfAKind;
            }

            if (isThreeOfAKind)
            {
                if(pairs.Count == 2)
                {
                    return HandCombinationTypesEnum.FullHouse;
                }

                return HandCombinationTypesEnum.ThreeOfAKind;
            }

            if (pairs.Count == 2)
            {
                return HandCombinationTypesEnum.TwoPair;
            }

            if(pairs.Count == 1 && pairs[0].Rank >= CardRanksEnum.Jack)
            {
                return HandCombinationTypesEnum.JacksOrBetter;
            }

            return HandCombinationTypesEnum.AllOther;
        }
    }
}
