using System;
using SlotMachine.Interfaces;

namespace SlotMachine
{
    public class ProbabilityGenerator : IProbabilityGenerator
    {
        private const string Apple = "A";
        private const string Banana = "B";
        private const string Pineapple = "P";
        private const string WildCard = "*";
        private const int NumberOfColumns = 3;
        public string GetRandomLineSymbols()
        {
            string value = string.Empty;
            for (int i = 0; i < NumberOfColumns; i++)
            {
                value = value + GetRandomValue();
            }
            return value;
        }
        private string GetRandomValue()
        {
            Random r = new Random();
            double randomNum = r.NextDouble();

            if (randomNum <= 0.05)
            { //5%
                return WildCard;
            }
            if (randomNum > 0.05 && randomNum <= 0.20) //15%
            {
                return Pineapple;
            }
            if (randomNum > 0.20 && randomNum <= 0.55)//35%
            {
                return Banana;
            }
            if (randomNum > 0.55 && randomNum < 1)//45%
            {
                return Apple;
            }
            return default;
        }

        public void TestRandomValues()
        {
            int count = 10000;
            double star = 0;
            double p = 0;
            double b = 0;
            double a = 0;

            for (int i = 0; i < count; i++)
            {

                string value = GetRandomValue();
                if (value == "*")
                {
                    star++;
                }
                if (value == "P")
                {
                    p++;
                }
                if (value == "B")
                {
                    b++;
                }
                if (value == "A")
                {
                    a++;
                }
            }

            Console.WriteLine($"*: {star / count * 100}%; P: {p / count * 100}%; B: {b / count * 100}%; A: {a / count * 100}%;");
        }
    }
}