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
        }

        private int Add(string inputString)
        {
            return 0;
        }
    }
}
