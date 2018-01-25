using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.Utils
{
    public static class RandomGenerator
    {
        private static readonly Random _random;

        public static int GetRandomInteger(int min = 0, int max = 9, int length = 4)
        {
            StringBuilder sb = new StringBuilder();
            for(int  i = 0; i < length; i++)
            {
                int randomInt = _random.Next(min, max + 1);
                sb.Append(randomInt);
            }
            return Int32.Parse(sb.ToString());
        }
        static RandomGenerator() {
            _random = new Random();
        }
    }
}
