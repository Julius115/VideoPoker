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

        // Method to return string representation of card
        private string ChangeForPrint()
        {
            string rank = null;
            string suit = null;

            if (Rank < CardRanks.Ten)
            {
                rank = ((int)Rank).ToString();
            }
            else if (Rank == CardRanks.Ten)
            {
                rank = "T";
            }
            else if (Rank == CardRanks.Jack)
            {
                rank = "J";
            }
            else if (Rank == CardRanks.Queen)
            {
                rank = "Q";
            }
            else if (Rank == CardRanks.King)
            {
                rank = "K";
            }
            else if (Rank == CardRanks.Ace)
            {
                rank = "A";
            }

            if (Suit == CardSuits.Diamonds)
            {
                suit = "♦";
            }
            else if (Suit == CardSuits.Clubs)
            {
                suit = "♣";
            }
            else if (Suit == CardSuits.Hearts)
            {
                suit = "♥";
            }
            else if (Suit == CardSuits.Spades)
            {
                suit = "♠";
            }

            return String.Concat(rank, suit);
        }

        public override string ToString()
        {
            return (ChangeForPrint());
        }
    }
}