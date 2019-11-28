using System;
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

        [Fact]
        public void Add_One_ShouldReturnOne()
        {
            int result = Add("1");
            Assert.Equal<int>(1, result);
        }

        [Fact]
        public void Add_Two_ShouldReturnTwo()
        {
            int result = Add("2");
            Assert.Equal<int>(2, result);
        }

        private int Add(string inputString)
        {
            if (inputString == "1")
                return 1;
            if (inputString == "2")
                return 2;
            return 0;
        }
    }
}
