using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegata01
{
    public delegate int TransFormer(int x);
    class Util
    {
        public static void TransFormer(int[] values,TransFormer t)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = { 1, 2, 3 };
            Util.TransFormer(values, square);
            foreach (var i in values)
            {
                Console.WriteLine(i+"  ");
            }
        }
        static int square(int x)
        {
            return x * x;
        }
        static int plus(int x)
        {
            return x * x;
        }
    }
}
