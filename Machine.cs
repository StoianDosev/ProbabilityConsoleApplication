using System;
using System.Collections.Generic;
using SlotMachine.Interfaces;

namespace SlotMachine
{
    public class Machine : IMachine
    {
        private readonly IProbabilityGenerator _generator;
        private readonly ISlotCalculator _calulator;
        private const int Rows = 4;        

        public Machine(IProbabilityGenerator probabilityGenerator, ISlotCalculator slotCalculator)
        {
            _generator = probabilityGenerator;
            _calulator = slotCalculator;
        }
        public void ExecuteTurn()
        {
            bool isCorectDeposit = false;
            double deposit = 0;
            do
            {
                Console.WriteLine("Please deposit money you would like to play with:");
                isCorectDeposit = double.TryParse(Console.ReadLine().Trim(), out deposit);
                if (!isCorectDeposit)
                {
                    Console.WriteLine("The amount is not correct. Please try again.");
                }
            } while (!isCorectDeposit);            
            

            while (deposit > 0)
            {
                Console.WriteLine("Enter stake amount:");
                double.TryParse(Console.ReadLine().Trim(), out double amount);

                var cuurentDeposit = WithdrawFromDeposit(deposit, amount);

                if (cuurentDeposit < 0)
                {
                    Console.WriteLine("Enter stake amount not greater than the deposit: " + deposit);
                    continue;
                }
                else
                {
                    deposit = cuurentDeposit;
                }

                double sumOfWinAmount = 0;
                IList<string> drawnRowsToCalculate = DrawRandomRows();
                sumOfWinAmount = CalculateAllWinRows(drawnRowsToCalculate, amount);
                Console.WriteLine("You have won: " + sumOfWinAmount);
                deposit = deposit + sumOfWinAmount;
                Console.WriteLine("Current balance is: " + deposit);
            }
            Console.WriteLine("Game over!");
        }

        private double CalculateAllWinRows(IList<string> drawnRowsToCalculate, double amount)
        {
            double sumOfWinAmount = 0;
            foreach (var value in drawnRowsToCalculate)
            {
                sumOfWinAmount += _calulator.CalculateWinAmount(value, amount);
            }
            return sumOfWinAmount;
        }

        private IList<string> DrawRandomRows()
        {
            IList<string> rows = new List<string>();
            string value = string.Empty;

            for (int i = 0; i < Rows; i++)
            {
                value = _generator.GetRandomLineSymbols();
                rows.Add(value);
                Console.WriteLine(value);
            }
            return rows;
        }

        private double WithdrawFromDeposit(double deposit, double amountForPlay)
        {
            if (deposit < amountForPlay)
            {
                return -1;
            }
            else
            {
                return deposit - amountForPlay;
            }
        }
    }
}