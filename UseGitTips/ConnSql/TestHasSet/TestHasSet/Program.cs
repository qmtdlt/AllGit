using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHasSet
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new List<int>();
            a.Add(1);
            a.Add(2);
            a.Add(3);
            List<int> b = new List<int>();
            b.Add(3);
            b.Add(4);
            b.Add(5);
            a.AddRange(b);
            HashSet<int> d = new HashSet<int>(a);
            foreach (var item in d)
            {
                Console.WriteLine(item);
            }
        }
    }
}
