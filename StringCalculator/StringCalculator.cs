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
        public void Add_JustOneNumber_ShouldReturnTheNumber()
        {
            int result = Add("1");
            Assert.Equal<int>(1, result);
        }

        private int Add(string inputString)
        {
            return 0;
        }
    }
}
