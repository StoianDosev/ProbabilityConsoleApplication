using Microsoft.Extensions.Options;
using SlotMachine.Interfaces;

namespace SlotMachine
{
    public class SlotCalculator : ISlotCalculator
    {
        private const char Apple = 'A';
        private const char Banana = 'B';
        private const char Pineapple = 'P';
        private const char WildCard = '*';
        private const int DefaultCountSymbolsOnRow = 3;       

        private readonly IOptions<CoefficientOptions> _coefficientOptions;
        public SlotCalculator(IOptions<CoefficientOptions> options)
        {
            _coefficientOptions = options;
        }
        public double CalculateWinAmount(string value, double ammount)
        {
            int numberOfWildcards = SymbolOccurance(value, WildCard);
            int numberApples = SymbolOccurance(value, Apple);
            int numberPineapples = SymbolOccurance(value, Pineapple);
            int numberBananas = SymbolOccurance(value, Banana);

            if (numberOfWildcards == DefaultCountSymbolsOnRow)
            {
                return 0;
            }

            double sum = 0;
            sum = numberOfWildcards * _coefficientOptions.Value.Wildcard * numberOfWildcards;

            int totalWiningAppleWithWildCard = numberApples + numberOfWildcards;
            if (totalWiningAppleWithWildCard == DefaultCountSymbolsOnRow) //AA*
            {
                sum += ammount * _coefficientOptions.Value.Apple * numberApples;
            }

            int totalWiningPineappleWithWildCard = numberPineapples + numberOfWildcards;
            if (totalWiningPineappleWithWildCard == DefaultCountSymbolsOnRow) //PP*
            {
                sum += ammount * _coefficientOptions.Value.Pineapple * numberPineapples;
            }

            int totalWiningBananasWithWildCard = numberBananas + numberOfWildcards;
            if (totalWiningBananasWithWildCard == DefaultCountSymbolsOnRow) //BB*
            {
                sum += ammount * _coefficientOptions.Value.Banana * numberBananas;
            }

            return sum;
        }

        private int SymbolOccurance(string value, char symbol)
        {
            var ar = value.ToCharArray();
            int count = 0;
            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i] == symbol)
                {
                    count++;
                }
            }
            return count;
        }
    }
}