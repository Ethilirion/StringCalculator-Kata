using System.Collections.Generic;
using System.Linq;

namespace Kata
{
    public class StringCalculatorImplementation : StringCalculator
    {
        public int Add(string inputString)
        {
            IEnumerable<char> separators = new List<char> { ',', '\n' };
            if (inputString == "")
                return 0;
            IEnumerable<int> numbers = from numberAsString
                                       in inputString.Split(separators.ToArray())
                                       select int.Parse(numberAsString);
            return numbers.Sum();
        }
    }
}