using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Dealer
    {
        public List<Card> deck = new List<Card>();
        public Card[] dealtCards = new Card[5];
        
        // Populates dealtCards empty elements with cards from deck
        public void DealCards()
        {
            for (int i = 0; i < 5; i++)
            {
                if (dealtCards[i] == null)
                {
                    dealtCards[i] = deck[0];
                    deck.Remove(deck[0]);
                }
            }
        }

        // Generates new deck and shuffles it
        public void ShuffleDeck()
        {
            List<Card> newDeck = new List<Card>();

            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card((CardRanks)i, (CardSuits)j);
                    newDeck.Add(card);
                }
            }

            List<Card> shuffledDeck = new List<Card>();

            Random random = new Random();

            int[] randomCardsIndexes = Enumerable.Range(0, 52).ToArray().OrderBy(x => random.Next()).ToArray();
            for (int i = 0; i < 52; i++)
            {
                shuffledDeck.Add(newDeck[randomCardsIndexes[i]]);
            }

            deck = shuffledDeck;
        }

        // Makes cards null in dealtCards, if its index wasn't specified in parameter 
        public void DiscardCards(List<int> inputOfCardsIndexesToKeep)
        {
            List<int> tempIndexes = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> cardsIndexesToChange = tempIndexes.Except(inputOfCardsIndexesToKeep).ToList();

            for (int i = 0; i < cardsIndexesToChange.Count; i++)
            {
                dealtCards[cardsIndexesToChange[i] - 1] = null;
            }

            DealCards();
        }
    }
}