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
        Dealer dealer = new Dealer();

        private int balance = 0;
        private int betSize = 0;
        private int result = 0;

        public void Start()
        {
            Console.WriteLine("Welcome to Video Poker \"Jacks or Better\" game\n");
            Console.WriteLine("Enter your starting balance:");

            balance = reader.GetBalance();

            Console.WriteLine();

            while (balance != 0)
            {
                InitializeGame();

                deck = dealer.GenerateDeck();

                dealtCards = new Card[5];

                dealer.DealCards(ref dealtCards, ref deck);

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + dealtCards[i].ToString());
                }

                dealer.ChangeCards(ref dealtCards, ref deck);

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
            betSize = reader.GetBetSize(ref balance);
            balance -= betSize;

            Console.Clear();
            Console.WriteLine("New game began\n");
            Console.WriteLine("Initial cards:\n");
        }
    }
}
