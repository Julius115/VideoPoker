using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Dealer
    {


        public void DealCards(ref Card[] dealtCards, ref List<Card> deck)
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

        public List<Card> GenerateDeck()
        {
            Random random = new Random();

            List<Card> deck = new List<Card>();

            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card((CardRanks)i, (CardSuits)j);
                    deck.Add(card);
                }
            }

            List<Card> shuffledDeck = new List<Card>();

            int[] randomCardsIndexes = Enumerable.Range(0, 52).ToArray().OrderBy(x => random.Next()).ToArray();
            for (int i = 0; i < 52; i++)
            {
                shuffledDeck.Add(deck[randomCardsIndexes[i]]);
            }

            return shuffledDeck;
        }

        public void ChangeCards(ref Card[] dealtCards, ref List<Card> deck)
        {
            Reader reader = new Reader();

            List<int> inputOfCardsIndexesToKeep = reader.GetIndexesToKeep();

            List<int> tempIndexes = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> cardsIndexesToChange = tempIndexes.Except(inputOfCardsIndexesToKeep).ToList();

            for (int i = 0; i < cardsIndexesToChange.Count; i++)
            {
                dealtCards[cardsIndexesToChange[i] - 1] = null;
            }

            DealCards(ref dealtCards, ref deck);
            return;
        }

    }
}
