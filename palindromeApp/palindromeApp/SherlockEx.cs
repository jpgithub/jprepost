using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindromeApp
{
    class SherlockEx
    {
        private static Dictionary<char, int> charTable = new Dictionary<char, int>();
        private static Dictionary<int, int> freqTable = new Dictionary<int, int>();
        /*
         * Complete the 'isValid' function below.
         *
         * The function is expected to return a STRING.
         * The function accepts STRING s as parameter.
         */

        public static string isValid(string s)
        {
            int min = 0;
            int max = 0;
            string msg = "NO";

            foreach (var c in s)
            {
                if (!charTable.ContainsKey(c))
                {
                    charTable.Add(c, 1);
                }
                else
                {
                    charTable[c] += 1;
                }
            }

            foreach (var k in charTable)
            {
                if (!freqTable.ContainsKey(k.Value))
                {
                    freqTable.Add(k.Value, 1);
                }
                else
                {
                    freqTable[k.Value] += 1;
                }
            }

            if (freqTable.Count == 1)
            {
                msg = "YES";
            }
            else if (freqTable.Count == 2)
            {
                var k1 = freqTable.Keys.ElementAt(0);
                var k2 = freqTable.Keys.ElementAt(1);

                if(Math.Abs(k1 - k2) == 1)
                 {
                    msg = "YES";
                }
             else if (k1 == 1 || k2 == 1)
                {
                    if (freqTable[1] == 1)
                    {
                        msg = "YES";
                    }
                }

            }


            return msg;
        }
    }
}
