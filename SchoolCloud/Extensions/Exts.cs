using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Extensions
{
    public static class Exts
    {
        public static int AppendZero(int ValueInt)
        {
            double value = ValueInt;
            int sign = 0;
            if (value < 0)
            {
                value = -value;
                sign = 0;
            }
            if (value <= 9)
            {
                return sign + 00000;
            }
            if (value <= 99)
            {
                return sign + 0000;
            }
            if (value <= 999)
            {
                return sign + 000;
            }
            if (value <= 9999)
            {
                return sign + 00;
            }
            if (value <= 99999)
            {
                return sign + 0;
            }
            if (value <= 999999)
            {
                return sign;
            }
            return sign ;
        }
    }
}
