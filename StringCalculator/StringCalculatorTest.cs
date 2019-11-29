using Kata;
using System;
using Xunit;

namespace Tests
{
    public class StringCalculatorTest
    {
        StringCalculator calculator;

        public StringCalculatorTest()
        {
            calculator = new StringCalculatorImplementation();
        }

        [Fact]
        public void Add_EmptyString_ShouldReturnZero()
        {
            int result = calculator.Add("");
            Assert.Equal<int>(0, result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        public void Add_SingleNumber_ShouldReturnCorrespondingInt(string input, int outputExpected)
        {
            int result = calculator.Add(input);
            Assert.Equal<int>(outputExpected, result);
        }

        [Fact]
        public void Add_TwoAndThreeSeparatedByComma_ShouldReturnFive()
        {
            int result = calculator.Add("3,2");
            Assert.Equal<int>(5, result);
        }

        [Fact]
        public void Add_TwoAndTwoSeparatedByComma_ShouldReturnFour()
        {
            int result = calculator.Add("2,2");
            Assert.Equal<int>(4, result);
        }

        [Fact]
        public void Add_ThreeAndThreeSeparatedByComma_ShouldReturnSix()
        {
            int result = calculator.Add("3,3");
            Assert.Equal<int>(6, result);
        }

        [Fact]
        public void Add_MultipleSeparatorsAndThreeNumbers_ShouldReturnSix()
        {
            int result = calculator.Add("1\n2,3");
            Assert.Equal<int>(6, result);
        }

        [Fact]
        public void Add_TooManySeparators_ShouldThrow()
        {
            Assert.Throws<Exception>(() => calculator.Add("1\n3,\n2"));
        }

        [Fact]
        public void Add_InitializeSemiColonSeparatorAndComputeTwoAndTwo_ShouldReturnFour()
        {
            int result = calculator.Add("//;\n2;2");
            Assert.Equal<int>(4, result);
        }

        [Fact]
        public void Add_InitializeUnderscoreSeparatorAndComputeTwoAndTwo_ShouldReturnFour()
        {
            int result = calculator.Add("//_\n2_2");
            Assert.Equal<int>(4, result);
        }
    }
}
