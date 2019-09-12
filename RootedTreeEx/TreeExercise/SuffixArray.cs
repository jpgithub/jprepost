using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeExercise
{
    public class SuffixArray
    {
        //Node<string> ansNode;
        private string givenstr;
        private List<string> ansList = new List<string>();
        public SuffixArray(string s)
        {
            givenstr = s;
            //GetSortedString = s.OrderBy(c => c);
        }

        public IOrderedEnumerable<char> GetSortedString { get; }

        public void AshtonString()
        {
            //ansNode = new Node<string>(string.Empty);
            //StringBuilder sortedstr = new StringBuilder();

            // using substring, solution for both repeated character and non-repeated character
            for (int i = 0; i < givenstr.Length; i++)
            {
                for (int j = i + 1; j <= givenstr.Length; j++)
                {
                    var ss = givenstr.Substring(i, j - i);
                    if (!ansList.Contains(ss))
                        ansList.Add(ss);
                }
            }

            ansList.Sort();

            int k = 1;
            char cc;
            string longstr = string.Empty;
            foreach(var item in ansList)
            {
                longstr += item;                
                if(longstr.Length >= k)
                {
                    cc = longstr[k - 1];
                    break;
                }
            }


            
            //for (int i = 0; i < ansList.Count; i++)
            //{
            //    var str = ansList[i];
            //    sum += ansList[i].Length;
            //    if (sum >= k)
            //    {
            //        cc = str[k - sum + 1];
            //        break;
            //    }
            //}

            // repeating character causes errors
            // level one
            //TriangleNumberApproach();

        }

        /// <summary>
        /// O(n^2)
        /// </summary>
        private void TriangleNumberApproach()
        {
            foreach (var c in givenstr.ToCharArray())
            {
                //ansNode.AddChild(c.ToString());
                ansList.Add(c.ToString());
                Console.WriteLine(c);
            }

            // level two
            char[] aaa = givenstr.ToCharArray();
            for (int i = 0; i + 1 < givenstr.Length; i++)
            {
                var union = (aaa[i].ToString() + aaa[i + 1].ToString());
                //ansList.Add(aaa[i].ToString());
                //ansList.Add(aaa[i+1].ToString());
                ansList.Add(union);

            }

            // duplicates startes here
            if (givenstr.Length > 2)
            {
                var nthset = ansList.GetRange(givenstr.Length, givenstr.Length - 1);
                CombineStringNodes(nthset, ansList);
            }
            else
            {
                ansList.Sort();
            }

            int sum = 0;
            char cc;
            for (int i = 0; i < ansList.Count; i++)
            {
                var str = ansList[i];
                sum += ansList[i].Length;
                if (sum == 3)
                {
                    cc = str[sum - i - 1];
                    break;
                }
            }
        }

        private static List<string> CombineStringNodes(List<string> nodelist)
        {
            List<string> combinedNodesList = new List<string>();
            for (int i = 0; i + 1 < nodelist.Count; i++)
            {
                var strA = nodelist[i].ToCharArray();
                var strB = nodelist[i + 1].ToCharArray();
                // using union to remove duplicates
                var union = strA.Union(strB).ToArray();
                combinedNodesList.Add(new string(union));
            }
            return combinedNodesList;
        }

        private static void CombineStringNodes(List<string> nodelist, List<string> ansList)
        {
            if(nodelist.Count == 1)
            {
                return;
            }

            List<string> combinedNodesList = new List<string>();
            for (int i = 0; i + 1 < nodelist.Count; i++)
            {
                var strA = nodelist[i].ToCharArray();
                var strB = nodelist[i + 1].ToCharArray();
                // using union to remove duplicates
                var union = strA.Union(strB).ToArray();
                combinedNodesList.Add(new string(union));
            }
            ansList.AddRange(combinedNodesList);
            ansList.Sort();
            CombineStringNodes(combinedNodesList, ansList);
        }
    }
}
