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
            GuardInputAgainstIncorrectValues(inputStringWithoutHeader, separators);
            IEnumerable<int> numbers = GetNumbersFromInput(inputStringWithoutHeader, separators);
            return ComputeNumbers(numbers);
        }

        private void GuardInputAgainstIncorrectValues(string inputStringWithoutHeader, IEnumerable<char> separators)
        {
            if (InputContainsNegativeCharacters(inputStringWithoutHeader, separators))
                throw new Exception("negatives not allowed");
            if (InputContainsIncorrectSymbols(inputStringWithoutHeader, separators))
                throw new Exception("incorrect separators");
            if (TooManySeparatorsForNumbers(inputStringWithoutHeader, separators))
                throw new Exception("too many separators");
        }

        private bool InputContainsNegativeCharacters(string inputStringWithoutHeader, IEnumerable<char> separators)
        {
            if (SeparatorsDoNotContainNegativeSymbol(separators) && InputStringContainsNegativeSymbol(inputStringWithoutHeader))
                return true;
            return false;
        }

        private bool InputStringContainsNegativeSymbol(string inputStringWithoutHeader)
        {
            return inputStringWithoutHeader.Contains('-');
        }

        private bool SeparatorsDoNotContainNegativeSymbol(IEnumerable<char> separators)
        {
            return separators.Contains('-') == false;
        }

        private bool InputContainsIncorrectSymbols(string inputStringWithoutHeader, IEnumerable<char> separators)
        {
            string correctSymbolsPattern = $@"^[\d{string.Concat(separators)}]+$";
            Regex correctSymbols = new Regex(correctSymbolsPattern);
            return correctSymbols.IsMatch(inputStringWithoutHeader) == false;
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

        private bool TooManySeparatorsForNumbers(string inputString, IEnumerable<char> separators)
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