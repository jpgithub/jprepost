using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindromeApp
{
    class SpecialString
    {
        public static long substrCount(int n, string s)
        {
            var substr = new StringBuilder();
            var memory = new Dictionary<int, string>();
            int substrtotal = s.Length;

            char currentchar = s[0];
            bool skip = false;

            var sstr = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                currentchar = s[i];
                substr.Append(currentchar);
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (currentchar == s[j] && !skip)
                    {
                        substr.Append(s[j]);
                        substrtotal++;
                    }
                    else if(skip && s.Length - j  > substr.Length)
                    {
                        //// check for 0...(n-2)X(n+2)...s.length pattern
                        var leftHalf = substr.ToString();
                        var rightHalf = s.Substring(j, substr.Length);
                        //// aaa X aaa
                        if(rightHalf == leftHalf)
                        {
                            substrtotal++;
                        }
                        break;
                    }
                    else
                    {
                        /// aXa bounded by two steps
                        if ((j+1) < s.Length && (j + 1) - i <= 2 && currentchar == s[j + 1])
                        {
                            substrtotal++;
                            break;
                        }
                        else
                        {
                            /// skip this character
                            skip = true;
                        }
                    }
                }
                skip = false;
                substr.Clear();
            }

            //for (int j = 1, k = j + 1; j < s.Length; j++)
            //{

            //    if(currentchar == s[j])
            //    {
            //        substrtotal++;
            //    }
            //    else
            //    {
            //        memory.Add(j - substr.Length, substr.ToString());
            //        substr.Clear();
            //    }
                
            //    if (currentchar == s[k])
            //    {
            //        substrtotal++;
            //    }


            //    if(k < s.Length)
            //    {
            //        k = j + 1;
            //    }

            //    currentchar = s[j];
            //    substr.Append(s[j]);
            //}
            
            return substrtotal;
        }

        // Complete the substrCount function below.
        //public static long substrCount(int n, string s)
        //{
        //    var substr = new StringBuilder();
        //    var memory = new Dictionary<int, string>();
        //    int substrtotal = s.Length;

        //    char currentchar = s[0];

        //    substr.Append(currentchar);

        //    int isAllsameminCount = 3;

        //    var pivot = new Stack<int>();
        //    for (int j = 1; j < s.Length; j++)
        //    {
        //        if ((j + 1) < s.Length && currentchar == s[j + 1])
        //        {
        //            /// aXa pattern
        //            //substr.Append(currentchar);
        //            //substr.Append(s[j + 1]);
        //            substrtotal++;
        //            if (s[j] != currentchar)
        //            {
        //                pivot.Push(j);
        //            }
        //        }

        //        if (currentchar == s[j])
        //        {
        //            /// aa pattern
        //            substrtotal++;
        //        }
        //        else
        //        {
        //            //// delimited
        //            //// minimum of 3 or more repeating char
        //            if (substr.Length >= isAllsameminCount)
        //            {
        //                substrtotal++;
        //            }

        //            memory.Add(j - substr.Length, substr.ToString());
        //            substr.Clear();
        //        }

        //        //// move to the next char
        //        currentchar = s[j];
        //        substr.Append(s[j]);
        //    }

        //    //// delimited
        //    //// minimum of 3 or more repeating char
        //    if(substr.ToString().All(c => c == substr[0]))
        //    {
        //        substrtotal++;
        //    }

    //    memory.Add(s.Length-substr.Length,substr.ToString());
    //    if(pivot.Count == 0)
    //    {
    //        if (substr.Length >= isAllsameminCount)
    //        {
    //            substrtotal++;
    //        }
    //    }

    //    while(pivot.Count() != 0)
    //    {
    //        var p = pivot.Pop();
    //        var stat = memory.TryGetValue(p + 1, out string right);
    //        if (stat && right.Length > 1)
    //        {
    //            memory.TryGetValue(p - right.Length, out string left);                    
    //            if (left == right)
    //            {
    //                substrtotal++;
    //            }
    //        }
    //    }

    //    return substrtotal;
    //}
}
}
