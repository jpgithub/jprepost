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
        private List<string> ansSetList = new List<string>();
        private List<string> ansSAList = new List<string>();
        private HashSet<string> ansSet = new HashSet<string>();
        public SuffixArray(string s)
        {
            givenstr = s;
            //GetSortedString = s.OrderBy(c => c);
        }

        public IOrderedEnumerable<char> GetSortedString { get; }
        public List<Tuple<int, int, int>> myTuple { get; private set; }

        public void AshtonString(int k)
        {
            //ansNode = new Node<string>(string.Empty);
            //StringBuilder sortedstr = new StringBuilder();


            //Rankingsystem();

            Ranking(givenstr);

            // using substring, solution for both repeated character and non-repeated character
            //for (int i = 0; i < givenstr.Length; i++)
            //{
            //    for (int j = i + 1; j <= givenstr.Length; j++)
            //    {
            //        var ss = givenstr.Substring(i, j - i);
            //        if (!ansList.Contains(ss))
            //        {
            //            // List Contain has poor speed
            //            ansList.Add(ss);
            //        }
            //    }
            //}

            //ansList.Sort();

            char cc = ' ';
            PartitionReduction(givenstr, ansSet);

            ansSetList = ansSet.ToList();

            ansSetList.Sort(cmpA);

            k--;
            int i = 0;
            bool exit = false;
            while (true)
            {
                for (int j = 0; j < ansSetList[i].Length; j++)
                {
                    var ss = ansSetList[i];
                    var id = ansSetList[i][j] - 'a';

                    int sub_len = j + 1;
                    if (k < sub_len)
                    {
                        cc = ss[k];
                        exit = true;
                        break;
                    }
                    k -= sub_len;

                }
                if(exit)
                {
                    break;
                }
                i++;
            }
            

            //int k = 3;
            //char cc;
            //string longstr = string.Empty;
            //foreach (var item in ansList)
            //{
            //    longstr += item;
            //    if (longstr.Length >= k)
            //    {
            //        cc = longstr[k - 1];
            //        break;
            //    }
            //}


        }

        private int cmpA(string x, string y)
        {
            return x.CompareTo(y);
        }

        private void Rankingsystem()
        {
            Ranking(givenstr);
            for (int i = 0; i < givenstr.Length; i++)
            {
                int cindx = myTuple[i].Item1;
                int fs = myTuple[i].Item2;
                int ss = myTuple[i].Item3;
                var sortedsubstr = string.Empty;
                if (ss != -1)
                {
                    sortedsubstr = givenstr.Substring(cindx);
                    ansSAList.Add(sortedsubstr);
                    sortedsubstr = givenstr.Substring(cindx);
                    ansSAList.Add(sortedsubstr);
                }
                else
                {
                    sortedsubstr = givenstr.Substring(cindx);
                    ansSAList.Add(sortedsubstr);
                }
            }

            int k = 3;
            char cc = ' ';
            bool exit = false;
            for (int i = 0; i < ansSAList.Count; i++)
            {
                var ss = ansSAList[i];
                int m = ss.Length;
                for (int j = 0; j < m; j++)
                {
                    var s2 = ss.Substring(0, j + 1);
                    if (ansSet.Contains(s2))
                    {
                        continue;
                    }
                    else
                    {
                        ansSet.Add(s2);
                        if (k <= s2.Length)
                        {
                            cc = s2[k - 1];
                            exit = true;
                            break;
                        }
                        k -= s2.Length;
                    }
                }
                if (exit)
                {
                    break;
                }
            }
        }

        public void Ranking(string s)
        {
            //List<Tuple<int, int, int>> sortedL = new List<Tuple<int, int, int>>();
            Tuple<int, int, int>[] L = new Tuple<int, int, int>[s.Length];
            List<int>[] ranking = new List<int>[26];
            for (int i = 0; i < s.Length; i++)
            {
                if (ranking[i] == null)
                {
                    ranking[i] = new List<int>(new int[1000000]);
                }
                ranking[0][i] = s[i] - 'a';
            }

            for(int cnt = 1, stp = 1; cnt < s.Length; cnt *=2, stp++)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ans = -1;
                    if (i + cnt < s.Length)
                        ans = ranking[stp - 1][i + cnt];

                    L[i] = Tuple.Create(i,
                        ranking[stp - 1][i],
                        ans);
                }

                Array.Sort(L,cmp);
                //sortedL = L.ToList();
                //sortedL.Sort(cmp);
                // Initialize rank for rank 0 suffix after sorting to its original index
                // in suffixRank array
                ranking[stp][L[0].Item1] = 0;

                for(int j = 1, currRank = 0; j < s.Length; j++)
                {
                    if(L[j-1].Item2 != L[j].Item2 || L[j-1].Item3 != L[j].Item3)
                    {
                        ++currRank;
                    }

                    int ind = L[j].Item1;
                    ranking[stp][ind] = currRank;
                }
            }

            this.myTuple = L.ToList();
        }

        private int cmp(Tuple<int, int, int> x, Tuple<int, int, int> y)
        {
            // first half
            if(x.Item2 == y.Item2)
            {
                // secondHalf
                return x.Item3.CompareTo(y.Item3);
            }
            else
            {
                return x.Item2.CompareTo(y.Item2);
            }
        }


        /// <summary>
        /// 0(N)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startingIndex"></param>
        /// <param name="endingIndex"></param>
        /// <param name="output"></param>
        private void PartitionReduction(string input, HashSet<string> output)
        {
            if (input == string.Empty)
            {
                return;
            }

            for (int i = 0; i < input.Length; i++)
            {
                string ssBegin = input.Substring(i);
                //string ssEnd = input.Substring(i, endingIndex - i);

                if (string.Empty != ssBegin )//&& !output.Contains(ssBegin))
                    output.Add(ssBegin);
                //if (string.Empty != ssEnd && !output.Contains(ssEnd))
                //    output.Add(ssEnd);
            }

            //int newEnd = endingIndex - 2;

            //if (newEnd < 0)
            //{
            //    return;
            //}

            //PartitionReduction(input.Substring(startingIndex + 1, newEnd), startingIndex, newEnd, output);
        }

        private void MirrorReduction(string input, int startingIndex, int endingIndex, List<string> output)
        {
            if(input == string.Empty)
            {
                return;
            }

            for (int i = 1; i <= endingIndex; i++)
            {
                string ssBegin = input.Substring(startingIndex, i);
                string ssEnd = input.Substring(i, endingIndex - i);

                if (string.Empty != ssBegin && !output.Contains(ssBegin))
                    output.Add(ssBegin);
                if (string.Empty != ssEnd && !output.Contains(ssEnd))
                    output.Add(ssEnd);
            }

            int newEnd = endingIndex - 2;

            if(newEnd < 0)
            {
                return;
            }

            MirrorReduction(input.Substring(startingIndex + 1, endingIndex - 2), startingIndex, newEnd, output);
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
