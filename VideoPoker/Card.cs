using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    public enum CardRanksEnum
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum CardSuitsEnum
    {
        Diamonds,
        Clubs,
        Hearts,
        Spades
    }

    class Card
    {
        public CardRanksEnum Rank { get; set; }
        public CardSuitsEnum Suit { get; set; }

        public Card(CardRanksEnum cardRank, CardSuitsEnum cardSuit)
        {
            Rank = cardRank;
            Suit = cardSuit;
        }
    }
}
