using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata
{
    public class StringCalculatorImplementation : StringCalculator
    {
        public int Add(string inputString)
        {
            if (InputStringIsEmpty(inputString))
                return 0;
            IEnumerable<char> separators = InitializeSeparators(inputString);
            if (TooManySeparators(inputString, separators))
                throw new Exception("too many separators");
            IEnumerable<int> numbers = GetNumbersFromInput(inputString, separators);
            return ComputeNumbers(numbers);
        }

        private bool TooManySeparators(string inputString, IEnumerable<char> separators)
        {
            int numberOfSeparators = inputString.Count(separator => separators.Contains(separator));
            int numberOfNumbers = GetNumbersFromInput(inputString, separators).Count();
            return numberOfSeparators >= numberOfNumbers;
        }

        private int ComputeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Sum();
        }

        private IEnumerable<int> GetNumbersFromInput(string inputString, IEnumerable<char> separators)
        {
            return from numberAsString
                   in inputString.Split(separators.ToArray())
                   where numberAsString != string.Empty
                   select int.Parse(numberAsString);
        }

        private IEnumerable<char> InitializeSeparators(string inputString)
        {
            return new List<char> { ',', '\n' };
        }

        private bool InputStringIsEmpty(string inputString)
        {
            return inputString == "";
        }
    }
}