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
        HandEvaluator handEvaluator = new HandEvaluator();

        private int balance = 0;
        private int betSize = 0;
        private int result = 0;

        public void Start()
        {
            view.PrintWelcome();

            balance = view.ReadBalance();

            Console.WriteLine();

            while (balance > 0)
            {
                dealer.dealtCards = new Card[5];

                betSize = view.ReadBetSize(ref balance);
                balance -= betSize;

                view.PrintNewGame();

                dealer.ShuffleDeck();

                dealer.DealCards();

                dealer.DiscardCards();

                HandCombinations handCombination = handEvaluator.EvaluateHand(dealer.SortCards());

                result = betSize * (int)handCombination;
                balance += result;

                view.PrintGameResult(handCombination, balance, result);

                Console.Clear();
            }
        }
    }
}
