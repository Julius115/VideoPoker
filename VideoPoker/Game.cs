using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Game
    {
        View view = new View();
        Dealer dealer = new Dealer();

        private int balance = 0;
        private int betSize = 0;
        private int result = 0;

        public void Start()
        {
            Console.WriteLine("Welcome to Video Poker \"Jacks or Better\" game\n");
            Console.WriteLine("Enter your starting balance:");

            balance = view.GetBalance();

            Console.WriteLine();

            while (balance != 0)
            {
                dealer.dealtCards = new Card[5];

                betSize = view.GetBetSize(ref balance);
                balance -= betSize;

                Console.Clear();
                Console.WriteLine("New game began\n");
                Console.WriteLine("Initial cards:\n");

                dealer.deck = dealer.GenerateDeck();

                dealer.DealCards();

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + dealer.dealtCards[i].ToString());
                }

                dealer.ChangeCards();

                Console.Clear();
                Console.WriteLine("Play cards after change:\n");

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + dealer.dealtCards[i].ToString());
                }

                HandCombinationTypes handCombination = HandCombination.GetHandCombination(dealer.dealtCards.ToList());

                result = betSize * (int)handCombination;
                balance += result;

                view.PrintGameResult(handCombination, balance, result);

                Console.Clear();
            }
        }
    }
}
