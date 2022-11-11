using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit
{
    internal class Num
    {
        public static int Rand(int min, int max)
        {
            Random rnd = new Random();
            if (min > max)
                return rnd.Next(max, min);
            return rnd.Next(min, max);
        }
    }
}
