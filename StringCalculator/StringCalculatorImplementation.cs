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
            AddNumbersData dataFromInput = InitializeSeparatorsAndInput(inputString);
            GuardInputAgainstIncorrectValues(dataFromInput);
            return ComputeSumForNumbersInInput(dataFromInput);
        }

        private AddNumbersData InitializeSeparatorsAndInput(string inputString)
        {
            IEnumerable<char> separators = InitializeDefaultSeparatorsOrFromHeader(inputString);
            string inputStringWithoutHeader = RemoveHeaderFromInputStringIfPresent(inputString);
            return new AddNumbersData { inputWithoutHeader = inputStringWithoutHeader, separators = separators };
        }

        private int ComputeSumForNumbersInInput(AddNumbersData dataFromInput)
        {
            IEnumerable<int> numbers = GetNumbersFromInput(dataFromInput);
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

        private void GuardInputAgainstIncorrectValues(AddNumbersData dataFromInput)
        {
            if (InputContainsNegativeCharacters(dataFromInput))
                throw new Exception("negatives not allowed");
            if (InputContainsIncorrectSymbols(dataFromInput))
                throw new Exception("incorrect separators");
            if (TooManySeparatorsForNumbers(dataFromInput))
                throw new Exception("too many separators");
        }

        private IEnumerable<int> GetNumbersFromInput(AddNumbersData dataFromInput)
        {
            return from numberAsString in dataFromInput.inputWithoutHeader.Split(dataFromInput.separators.ToArray())
                   where numberAsString != string.Empty
                   select int.Parse(numberAsString);
        }

        private bool InputContainsNegativeCharacters(AddNumbersData dataFromInput)
        {
            if (SeparatorsDoNotContainNegativeSymbol(dataFromInput.separators) && InputStringContainsNegativeSymbol(dataFromInput.inputWithoutHeader))
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

        private bool InputContainsIncorrectSymbols(AddNumbersData dataFromInput)
        {
            string correctSymbolsPattern = $@"^[\d{string.Concat(dataFromInput.separators)}]+$";
            Regex correctSymbols = new Regex(correctSymbolsPattern);
            return correctSymbols.IsMatch(dataFromInput.inputWithoutHeader) == false;
        }

        private string CleansedInputString(string inputString)
        {
            int indexHeaderEnding = inputString.IndexOf(addHeaderEnding);
            return inputString.Substring(indexHeaderEnding + 1);
        }

        private bool TooManySeparatorsForNumbers(AddNumbersData dataFromInput)
        {
            int numberOfSeparators = CountNumberOfSeparators(dataFromInput);
            int numberOfNumbers = CoutNumberOfNumbers(dataFromInput);
            return numberOfSeparators >= numberOfNumbers;
        }

        private int CoutNumberOfNumbers(AddNumbersData dataFromInput)
        {
            return GetNumbersFromInput(dataFromInput).Count();
        }

        private int CountNumberOfSeparators(AddNumbersData dataFromInput)
        {
            return dataFromInput.inputWithoutHeader.Count(separator => dataFromInput.separators.Contains(separator));
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