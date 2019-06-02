using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class View
    {
        public List<int> ReadIndexesToKeep()
        {
            List<int> inputOfCardsIndexesToKeep = new List<int>();
            bool isInputValid;

            do
            {
                Console.WriteLine("\nChoose cards to hold (numbers separated with single space):");
                string[] tempinputOfCardsIndexesToKeep = Console.ReadLine().Split(' ');

                if (tempinputOfCardsIndexesToKeep.Length == 1 && String.IsNullOrEmpty(tempinputOfCardsIndexesToKeep[0]))
                {
                    break;
                }

                isInputValid = IsDigitsOnly(tempinputOfCardsIndexesToKeep);

                if (isInputValid)
                {
                    inputOfCardsIndexesToKeep = Array.ConvertAll(tempinputOfCardsIndexesToKeep, int.Parse).ToList();
                    if (inputOfCardsIndexesToKeep.Any(a => a > 5 || a < 1) || inputOfCardsIndexesToKeep.Count > 5)
                    {
                        isInputValid = false;
                    }
                }

            } while (!isInputValid);

            return inputOfCardsIndexesToKeep;
        }

        public int ReadBalance()
        {
            int balance;
            string tempBalance = Console.ReadLine();

            while (!Int32.TryParse(tempBalance, out balance) || balance <= 0)
            {
                if (Int32.TryParse(tempBalance, out int a) && a <= 0)
                {
                    Console.WriteLine("Balance must be positive");
                }
                else
                {
                    Console.WriteLine("Enter your starting balance as a number: ");
                }

                tempBalance = Console.ReadLine();
            }

            Console.Clear();

            return balance;
        }

        public int ReadBetSize(ref int balance)
        {
            Console.WriteLine("Your balance: " + balance + "\n");
            Console.WriteLine("Enter bet size:");
            string tempBetSize = Console.ReadLine();
            int betSize;

            while (!Int32.TryParse(tempBetSize, out betSize) || betSize <= 0 || ((balance - betSize) < 0))
            {
                if (!Int32.TryParse(tempBetSize, out int a))
                {
                    Console.WriteLine("Enter bet size as a number:");
                }
                else if (betSize <= 0)
                {
                    Console.WriteLine("Bet size must be positive");
                    Console.WriteLine("Enter bet size:");
                }
                else if ((balance - betSize) < 0)
                {
                    Console.WriteLine("Your entered bet size: " + betSize + " is bigger than your balance: " + balance);
                    Console.WriteLine("Enter bet size between 1 and " + balance);
                }

                tempBetSize = Console.ReadLine();
            }

            return betSize;
        }

        public void PrintWelcome()
        {
            Console.WriteLine("Welcome to Video Poker \"Jacks or Better\"\n");
            Console.WriteLine("Enter your starting balance:");
        }

        public void PrintNewGame()
        {
            Console.Clear();
            Console.WriteLine("Your hand:\n");
        }

        public void PrintGameResult(HandCombinations handCombination, int balance, int result)
        {
            Console.WriteLine("\n" + handCombination + "\n");
            if (result == 0)
            {
                Console.WriteLine("You have lost, your balance now is: " + balance);
            }
            else
            {
                Console.WriteLine("You have won: " + result + ", your balance now is: " + balance);
            }

            if (balance == 0)
            {
                Console.WriteLine("\nYou lost all your balance\n");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

                return;
            }

            Console.WriteLine("\nPress any key to play again");
            Console.ReadKey();

            Console.Clear();
        }

        public void DisplayCardsAfterChange(Card[] dealtCards)
        {
            Console.Clear();

            Console.WriteLine("Your hand after draw:\n");
            
            DisplayCards(dealtCards);
        }

        public void DisplayCards(Card[] dealtCards)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine((i + 1) + ": " + dealtCards[i].ToString());
            }
        }

        private bool IsDigitsOnly(string[] str)
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
