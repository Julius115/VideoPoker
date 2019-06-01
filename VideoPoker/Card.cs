using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    public enum CardRanks
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

    public enum CardSuits
    {
        Diamonds,
        Clubs,
        Hearts,
        Spades
    }

    class Card
    {
        public CardRanks Rank { get; set; }
        public CardSuits Suit { get; set; }

        public Card(CardRanks cardRank, CardSuits cardSuit)
        {
            Rank = cardRank;
            Suit = cardSuit;
        }

        public override string ToString()
        {
            return (this.Rank + " of " + this.Suit);
        }
    }

    

}
