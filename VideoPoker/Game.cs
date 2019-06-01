using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Game
    {
        List<Card> deck = new List<Card>();
        Card[] dealtCards = new Card[5];

        Reader reader = new Reader();

        Random random = new Random();

        public static int balance = 0;
        public static int betSize = 0;
        private int result = 0;

        public void Start()
        {
            Console.WriteLine("Welcome to Video Poker \"Jacks or Better\" game\n");
            Console.WriteLine("Enter your starting balance:");

            balance = reader.GetBalance();

            Console.WriteLine();

            while (balance != 0)
            {
                deck = GenerateDeck();
                dealtCards = new Card[5];

                DealCards();

                InitializeGame();

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + dealtCards[i].ToString());
                }

                ChangeCards();

                Console.Clear();
                Console.WriteLine("Play cards after change:\n");

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + dealtCards[i].ToString());
                }

                PrintGameResult(dealtCards.Take(5).ToList());

                Console.Clear();
            }
        }

        private void DealCards()
        {
            for(int i = 0; i < 5; i++)
            {
                if(dealtCards[i] == null)
                {
                    dealtCards[i] = deck[0];
                    deck.Remove(deck[0]);
                }
            }
        }

        private void PrintGameResult(List<Card> playCards)
        {
            HandCombinationTypes handCombination = HandCombination.GetHandCombination(playCards);

            result = betSize * (int)handCombination;
            balance += result;

            Console.WriteLine("\n" + handCombination + "\n");
            Console.WriteLine("You have won : " + result + ", your balance now is: " + balance);

            if (balance == 0)
            {
                Console.WriteLine("\nYou lost all your balance\n");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPress any key to play again");

            Console.ReadKey();
        }

        private void InitializeGame()
        {
            betSize = reader.GetBetSize();
            balance -= betSize;

            Console.Clear();
            Console.WriteLine("New game began\n");
            Console.WriteLine("Initial cards:\n");
        }


        private void ChangeCards()
        {
            List<int> inputOfCardsIndexesToKeep = reader.GetIndexesToKeep();

            List<int> tempIndexes = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> cardsIndexesToChange = tempIndexes.Except(inputOfCardsIndexesToKeep).ToList();

            for (int i = 0; i < cardsIndexesToChange.Count; i++)
            {
                dealtCards[cardsIndexesToChange[i]-1] = null;
            }

            DealCards();
            return;
        }

        private List<Card> GenerateDeck()
        {
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

        public static bool IsDigitsOnly(string[] str)
        {
            foreach (string s in str)
            {
                if (!int.TryParse(s, out int n))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
