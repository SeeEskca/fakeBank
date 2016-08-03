using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB
{
    public class Math
    {
        public int addIntegers(int x, int y)
        {
            int sum = x;
            for (int i = 0; i < y; i++)
                sum += 1;

            return sum;
        }
    }
}
