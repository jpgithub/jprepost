using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsequenceWeighting
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            //List<int> A = new List<int>();
            //List<int> W = new List<int>();
            //string[] lines = System.IO.File.ReadAllLines(@"TestCase.txt");
            //A = Array.ConvertAll(lines[1].Split(' '), Int32.Parse).ToList();
            //W = Array.ConvertAll(lines[2].Split(' '), Int32.Parse).ToList();

            List<(int, int)> sequence = new List<(int, int)>() { (1, 10), (2, 20), (3, 30), (4, 40), (1, 15), (2, 15), (3, 15), (4, 50) };
            
            var stckdict = new Stack<Dictionary<int, int>>();

            //var dsubseq = new Dictionary<int, Stack<(int, int)>>();

            var lsubseq = new List<WeightedStack>();

            //var subs = CreatSubsequence(sequence.ToList());
            
            for (int i = 0; i < sequence.Count; i++)
            {
                if(sequence.Count - 1 - i == 0)
                {
                    break;
                }
                var subs1 = CreatSubsequenceWS(sequence.ToList(), sequence.Count - 1 - i);
                
                lsubseq.Add(subs1);
            }

            lsubseq.Sort((a,b)=>
            {
                if(a.Count > b.Count)
                {
                    return 1;
                }
                else if( a.Count < b.Count)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });

            uint maxweight = 0;
            for (int i = 0; i < lsubseq.Count; i++)
            {
                if((i + 1) < lsubseq.Count)
                {
                    var c = lsubseq[i].GetBase;
                    var n = lsubseq[i+1].GetBase;
                    //// Same length and matching key
                    if(c.Item1 == n.Item1 && lsubseq[i].Count == lsubseq[i+1].Count)
                    {
                        if (c.Item2 > n.Item2)
                        {
                            lsubseq[i + 1].BaseCorrection(c);
                        }
                    }
                }
                if(maxweight < lsubseq[i].Weight)
                {
                    maxweight = lsubseq[i].Weight;
                }
            }

            Console.Out.WriteLine(maxweight);

            //long result = solve(A, W);
        }


        private static WeightedStack CreatSubsequenceWS(List<(int, int)> sequence, int startIndex)
        {
            var wsseq = new WeightedStack() { StartingIndex = startIndex };
            for (int i = startIndex; i >= 0; i--)
            {
                ////// Key should be increasing while weight should be decreasing
                if (wsseq.Count == 0)
                {
                    wsseq.Push(sequence[i]);
                }
                else
                {
                    var e = wsseq.Peek();
                    if (e.Item1 > sequence[i].Item1)
                    {
                        wsseq.Push(sequence[i]);
                    }
                }
            }

            return wsseq;
        }

        private static Stack<(int, int)> CreatSubsequence(List<(int, int)> sequence, int startIndex)
        {
            var ssubseq = new Stack<(int, int)>();
            for (int i = startIndex; i >= 0; i--) 
            {
                //// Key should be increasing while weight should be decreasing
                if (ssubseq.Count == 0)
                {
                    ssubseq.Push(sequence[i]);
                }
                else
                {
                    var e = ssubseq.Peek();
                    if (e.Item1 > sequence[i].Item1)
                    {
                        ssubseq.Push(sequence[i]);
                    }
                }
            }
            return ssubseq;
        }

        // Complete the solve function below.
        //static long solve(int[] Ai, int[] Wi)
        //{
        //    List<int> A = Ai.ToList();
        //    long maxSummedWeight = 0;

        //    var WeightTable = new Dictionary<int, WeightNode>();
        //    for (int i = 0; i < Ai.Length; i++)
        //    {
        //        if (!WeightTable.ContainsKey(Ai[i]))
        //        {
        //            var wn = new WeightNode();
        //            wn.Childrens.Add(Tuple.Create(i, Wi[i]));
        //            WeightTable.Add(Ai[i], wn);
        //        }
        //        else
        //        {
        //            WeightTable[Ai[i]].Childrens.Add(Tuple.Create(i, Wi[i]));
        //        }
        //    }

        //    Dictionary<int, long> graphTable = new Dictionary<int, long>();

        //    for (int i = Ai.Length - 1; i >= 0; i--)
        //    {
        //        var a = Ai[i];
        //        var child = A.GetRange(i, A.Count - i).Where(e => e > a);
        //        if (!graphTable.ContainsKey(a))
        //        {
        //            if (child.Count() == 0)
        //            {
        //                graphTable.Add(a, WeightTable[a].GetWeight(i));
        //            }
        //            else
        //            {
        //                long ww = WeightTable[a].GetWeight(i);
        //                long localmax = 0;
        //                foreach (var c in child)
        //                {
        //                    long ans = graphTable[c];
        //                    if (ans > localmax)
        //                    {
        //                        localmax = ans;
        //                    }
        //                }
        //                ww += localmax;

        //                graphTable.Add(a, ww);
        //            }
        //        }
        //        else
        //        {
        //            long ww = WeightTable[a].GetWeight(i);

        //            long localmax = 0;
        //            // child contain multiple 40
        //            foreach (var c in child)
        //            {
        //                long ans = graphTable[c];
        //                if (ans > localmax)
        //                {
        //                    localmax = ans;
        //                }
        //            }
        //            ww += localmax;

        //            if (ww > graphTable[a])
        //            {
        //                graphTable[a] = ww;
        //                // Might have to reset or do a check data sanity check
        //            }
        //        }

        //        if (graphTable[a] > maxSummedWeight)
        //        {
        //            maxSummedWeight = graphTable[a];
        //        }
        //    }
        //    return maxSummedWeight;
        //}

        public static Func<int, bool> GreaterThanReferenceElement(int element)
        {
            return e => e > element;
        }

        public static Func<int, bool> LesserThanReferenceElement(int element)
        {
            return e => e < element;
        }

        public static Func<int, bool> GreaterIndexPredicate(List<int> A, int maxWeightedIndex)
        {
            return e => A.IndexOf(e) > maxWeightedIndex;
        }

        public static Func<int, bool> LesserIndexPredicate(List<int> A, int maxWeightedIndex)
        {
            return e => A.IndexOf(e) < maxWeightedIndex;
        }

        public static List<int> GenerateSubsequence(List<int> A, Func<int, bool> predicate, Func<int, bool> indexpredicate)
        {
            //Check how many element have greater than maxWeightA
            var samples = A.Where(predicate);
            //Among these sample how many are have index less than max weight index?
            return samples.Where(indexpredicate).ToList();
        }

        public static WeightedSubsequence GetSequenceWeight(int weightedA, List<int> keepList, int startIndex, out int revisitIndex)
        {
            WeightedSubsequence wseq = new WeightedSubsequence(weightedA);
            revisitIndex = -1;
            for (int i = startIndex; i < keepList.Count; i++)
            {
                if (wseq.TryInsert(keepList[i]) == -1)
                {
                    if (revisitIndex < 0)
                    {
                        revisitIndex = i;
                    }
                }
            }
            return wseq;
        }

        public static List<int> GenerateWeightedSubsequence(List<int> A, List<int> W, int maxWeightedIndex)
        {
            List<int> Asubseq;
            int[] Wi = W.ToArray();
            long weightedsum = 0 - Wi[maxWeightedIndex];

            WeightedSubsequence.CreateTable(A.ToArray());

            int maxWeightedA = A[maxWeightedIndex];

            var U = GenerateSubsequence(A, GreaterThanReferenceElement(maxWeightedA), GreaterIndexPredicate(A, maxWeightedIndex));

            U.Sort(delegate(int x, int y)
            {
                if (W[A.IndexOf(x)] < W[A.IndexOf(y)])
                {
                    return -1;
                }
                else if (W[A.IndexOf(x)] > W[A.IndexOf(y)])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            int startIndex = 0;
            long lc = 0;
            List<int> selectedSeq = new List<int>();

            while (startIndex != -1)
            {
                //Pivot
                int revisitIndex;
                var c = GetSequenceWeight(maxWeightedA, U, startIndex, out revisitIndex);
                var ans = c.CalculateWeight(Wi);
                startIndex = revisitIndex;

                if (ans > lc)
                {
                    lc = ans;
                    selectedSeq = c.Data.ToList();
                }
            }

            Asubseq = new List<int>(selectedSeq);
            weightedsum += lc;

            //reset
            startIndex = 0;
            lc = 0;
            selectedSeq.Clear();

            var L = GenerateSubsequence(A, LesserThanReferenceElement(maxWeightedA), LesserIndexPredicate(A, maxWeightedIndex));

            L.Sort(delegate(int x, int y)
            {
                if (W[A.IndexOf(x)] < W[A.IndexOf(y)])
                {
                    return -1;
                }
                else if (W[A.IndexOf(x)] > W[A.IndexOf(y)])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            while (startIndex != -1)
            {
                // Pivot
                int revisitIndex;
                var c = GetSequenceWeight(maxWeightedA, L, startIndex, out revisitIndex);
                var ans = c.CalculateWeight(Wi);
                startIndex = revisitIndex;

                if (ans > lc)
                {
                    lc = ans;
                    selectedSeq = c.Data.ToList();
                }
            }

            weightedsum += lc;
            Asubseq = selectedSeq.Union(Asubseq).ToList();

            return Asubseq;
        }
    }
}
