using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindromeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /// 0:25
            /// 1:15
            /// 
            string[] argss = { "bxbxxxxaaaa", "aaaaa", "bxxxoxxx", "bxxoxox", "baaaaxxxx", "baaaa", "baaaaxxxxcd" };
            string input = argss[2];
            SpecialString.substrCount(input.Length,input);
        }
    }
}
