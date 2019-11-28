using System.Collections.Generic;
using System.Linq;

namespace Kata
{
    public class StringCalculatorImplementation : StringCalculator
    {
        public int Add(string inputString)
        {
            if (inputString == "")
                return 0;
            IEnumerable<int> numbers = from numberAsString in inputString.Split(',')
                                       select int.Parse(numberAsString);
            return numbers.Sum();
        }
    }
}