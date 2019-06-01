﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Game
    {
        List<Card> deck = new List<Card>();
        Random random = new Random();

        private int balance = 0;
        private int betSize = 0;
        private int result = 0;

        public void Start()
        {
            deck = GenerateDeck();

            Console.WriteLine("Welcome to Video Poker \"Jacks or Better\" game\n");
            Console.WriteLine("Enter your starting balance:");

            balance = GetBalance();

            Console.WriteLine();

            while (true)
            {
                List<Card> playCards = GeneratePlayCards();

                betSize = GetBetSize();
                balance -= betSize;

                Console.Clear();
                Console.WriteLine("New game began\n");
                Console.WriteLine("Initial cards:\n");

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + playCards[i].ToString());
                }

                ChangeCards(ref playCards);

                Console.Clear();
                Console.WriteLine("Play cards after change:\n");

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine((i + 1) + ": " + playCards[i].ToString());
                }

                HandCombinationTypes handCombination = HandCombination.GetHandCombination(playCards.Take(5).ToList());

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
                Console.Clear();
            }
        }

        private void ChangeCards(ref List<Card> playCards)
        {

            List<int> inputOfCardsIndexesToKeep = GetIndexesToKeep();

            List<int> tempIndexes = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> cardsIndexesToChange = tempIndexes.Except(inputOfCardsIndexesToKeep).ToList();

            for (int i = 0; i < cardsIndexesToChange.Count; i++)
            {
                playCards[cardsIndexesToChange[i]-1] = playCards[i + 5];
            }

        }

        private List<int> GetIndexesToKeep()
        {
            List<int> inputOfCardsIndexesToKeep = new List<int>();
            bool isInputValid;

            do
            {
                Console.WriteLine("\nEnter indexes of cards you want to keep(numbers separated with single space):");
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

        private int GetBalance()
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
            return balance;
        }

        private List<Card> GeneratePlayCards()
        {
            int[] randomizedCardsIndexes = Enumerable.Range(0, 52).ToArray().OrderBy(x => random.Next()).ToArray();
            List<Card> playCards = new List<Card>();

            for (int i = 0; i < 10; i++)
            {
                playCards.Add(deck[randomizedCardsIndexes[i]]);
            }

            return playCards;
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

            return deck;
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

        private int GetBetSize()
        {
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

    }
}
