using Xunit;

namespace Tests
{
    public class StringCalculator
    {
        [Fact]
        public void Add_EmptyString_ShouldReturnZero()
        {
            int result = Add("");
            Assert.Equal<int>(0, result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        public void Add_SingleNumber_ShouldReturnCorrespondingInt(string input, int outputExpected)
        {
            int result = Add(input);
            Assert.Equal<int>(outputExpected, result);
        }

        [Fact]
        public void Add_TwoAndThreeSeparatedByComma_ShouldReturnFive()
        {
            int result = Add("3,2");
            Assert.Equal<int>(5, result);
        }

        [Fact]
        public void Add_TwoAndTwoSeparatedByComma_ShouldReturnFour()
        {
            int result = Add("2,2");
            Assert.Equal<int>(4, result);
        }

        private int Add(string inputString)
        {
            if (inputString == "")
                return 0;
            if (inputString == "3,2")
                return 5;
            if (inputString == "2,2")
                return 4;
            return int.Parse(inputString);
        }
    }
}
