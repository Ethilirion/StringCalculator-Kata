using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kata
{
    public class StringCalculatorImplementation : StringCalculator
    {
        private string addHeaderBegining = "//";
        private string addHeaderEnding = "\n";

        public int Add(string inputString)
        {
            if (InputStringIsEmpty(inputString))
                return 0;
            IEnumerable<char> separators = InitializeSeparators(inputString);
            string inputStringWithoutHeader = RemoveHeaderFromInputString(inputString);
            if (TooManySeparators(inputStringWithoutHeader, separators))
                throw new Exception("too many separators");
            IEnumerable<int> numbers = GetNumbersFromInput(inputStringWithoutHeader, separators);
            return ComputeNumbers(numbers);
        }

        private string RemoveHeaderFromInputString(string inputString)
        {
            if (InputStringContainsHeader(inputString))
                return CleansedInputString(inputString);
            return inputString;
        }

        private string CleansedInputString(string inputString)
        {
            int indexHeaderEnding = inputString.IndexOf(addHeaderEnding);
            return inputString.Substring(indexHeaderEnding + 1);
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
            if (InputStringContainsHeader(inputString))
                return InitializeSeparatorsFromHeader(inputString);
            return new List<char> { ',', '\n' };
        }

        private IEnumerable<char> InitializeSeparatorsFromHeader(string inputString)
        {
            string headerPattern = $"^{addHeaderBegining}(.){addHeaderEnding}";
            Regex headerRegex = new Regex(headerPattern);
            var headerMatchGroups = headerRegex.Match(inputString);
            char newSeparator = headerMatchGroups.Groups[1].Value[0];
            return new List<char> { newSeparator };
        }

        private bool InputStringContainsHeader(string inputString)
        {
            return inputString.StartsWith(addHeaderBegining) &&
                inputString.Contains(addHeaderEnding);
        }

        private bool InputStringIsEmpty(string inputString)
        {
            return inputString == "";
        }
    }
}