using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kata
{
    public class StringCalculatorImplementation : StringCalculator
    {
        private readonly string addHeaderBegining = "//";
        private readonly string addHeaderEnding = "\n";

        public int Add(string inputString)
        {
            if (InputStringIsEmpty(inputString))
                return DefaultValue();
            InputAndSeparators inputAndSeparators = InitializeSeparatorsAndInput(inputString);
            GuardInputAgainstIncorrectValues(inputAndSeparators);
            return ComputeSumForNumbersInInput(inputAndSeparators);
        }

        private InputAndSeparators InitializeSeparatorsAndInput(string inputString)
        {
            IEnumerable<char> separators = InitializeDefaultSeparatorsOrFromHeader(inputString);
            string inputStringWithoutHeader = RemoveHeaderFromInputStringIfPresent(inputString);
            return new InputAndSeparators { inputWithoutHeader = inputStringWithoutHeader, separators = separators };
        }

        private int ComputeSumForNumbersInInput(InputAndSeparators inputAndSeparators)
        {
            IEnumerable<int> numbers = GetNumbersFromInput(inputAndSeparators);
            return ComputeSum(numbers);
        }

        private IEnumerable<char> InitializeDefaultSeparatorsOrFromHeader(string inputString)
        {
            if (InputStringContainsHeader(inputString))
                return InitializeSeparatorsFromHeader(inputString);
            return new List<char> { ',', '\n' };
        }

        private string RemoveHeaderFromInputStringIfPresent(string inputString)
        {
            if (InputStringContainsHeader(inputString))
                return CleansedInputString(inputString);
            return inputString;
        }

        private void GuardInputAgainstIncorrectValues(InputAndSeparators inputAndSeparators)
        {
            if (InputContainsNegativeCharacters(inputAndSeparators))
                throw new Exception("negatives not allowed");
            if (InputContainsIncorrectSymbols(inputAndSeparators))
                throw new Exception("incorrect separators");
            if (TooManySeparatorsForNumbers(inputAndSeparators))
                throw new Exception("too many separators");
        }

        private IEnumerable<int> GetNumbersFromInput(InputAndSeparators inputAndSeparators)
        {
            return from numberAsString in inputAndSeparators.inputWithoutHeader.Split(inputAndSeparators.separators.ToArray())
                   where numberAsString != string.Empty
                   select int.Parse(numberAsString);
        }

        private bool InputContainsNegativeCharacters(InputAndSeparators inputAndSeparators)
        {
            if (SeparatorsDoNotContainNegativeSymbol(inputAndSeparators.separators) && InputStringContainsNegativeSymbol(inputAndSeparators.inputWithoutHeader))
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

        private bool InputContainsIncorrectSymbols(InputAndSeparators inputAndSeparators)
        {
            string correctSymbolsPattern = $@"^[\d{string.Concat(inputAndSeparators.separators)}]+$";
            Regex correctSymbols = new Regex(correctSymbolsPattern);
            return correctSymbols.IsMatch(inputAndSeparators.inputWithoutHeader) == false;
        }

        private string CleansedInputString(string inputString)
        {
            int indexHeaderEnding = inputString.IndexOf(addHeaderEnding);
            return inputString.Substring(indexHeaderEnding + 1);
        }

        private bool TooManySeparatorsForNumbers(InputAndSeparators inputAndSeparators)
        {
            int numberOfSeparators = CountNumberOfSeparators(inputAndSeparators);
            int numberOfNumbers = CoutNumberOfNumbers(inputAndSeparators);
            return numberOfSeparators >= numberOfNumbers;
        }

        private int CoutNumberOfNumbers(InputAndSeparators inputAndSeparators)
        {
            return GetNumbersFromInput(inputAndSeparators).Count();
        }

        private int CountNumberOfSeparators(InputAndSeparators inputAndSeparators)
        {
            return inputAndSeparators.inputWithoutHeader.Count(separator => inputAndSeparators.separators.Contains(separator));
        }

        private int ComputeSum(IEnumerable<int> numbers)
        {
            return numbers.Sum();
        }

        private int DefaultValue()
        {
            return 0;
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