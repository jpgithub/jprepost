using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsequenceWeighting
{
    public class WeightedSubsequence
    {
        private static Dictionary<int, int> inverseTable;
        private static Dictionary<int, int> inverseWeightTable;
        
        private List<int> subsequence = new List<int>();

        public static void CreateTable(int[] A)
        {
            inverseTable = new Dictionary<int, int>();
            // Convert Array to Dictionary
            for (int i = 0; i < A.Length; i++)
            {
                inverseTable.Add(A[i], i);
            }
        }

        public static void CreateWeightTable(int[] W, int[] A)
        {
            inverseWeightTable = new Dictionary<int, int>();
            // Convert Array to Dictionary
            if (A.Length != W.Length)
            {
                throw new ArgumentException("Length Mismatch, Must have same Length");
            }

            for (int i = 0; i < A.Length; i++)
            {
                inverseWeightTable.Add(W[i], A[i]);
            }
        }

        public WeightedSubsequence(int pivot)
        {
            Pivot = pivot;
            subsequence.Add(pivot);
        }
        
        public int Pivot { get; set; }

        public int PivotIndex
        {
            get
            {
                if (inverseTable != null)
                {
                    return inverseTable[Pivot];
                }
                else
                {
                    return inverseWeightTable[Pivot];
                }
            }
        }

        public List<int> Data
        {
            get
            {
                return subsequence;
            }
        }

        public long CalculateWeight(int[] W)
        {
            long sum = 0;
            foreach(var a in subsequence)
            {
                sum += W[inverseTable[a]];
            }
            return sum;
        }

        public List<int> FetchSubSequence(int[] W)
        {
            List<int> indexsubseq = new List<int>();

            // loop throught the weight subsequence to lok at the associate "A" index subsequence
            foreach (var w in subsequence)
            {
                indexsubseq.Add(inverseWeightTable[w]);
            }
            return indexsubseq;
        }

        public int TryWeightInsert(int w)
        {
            int success = -1;
            int currentPivotIndex = subsequence.IndexOf(Pivot);

            int pPlusOne = currentPivotIndex;
            int pMinusOne = currentPivotIndex;

            if (currentPivotIndex + 1 < subsequence.Count)
            {
                pPlusOne += 1;
            }

            if (currentPivotIndex - 1 > 0)
            {
                pMinusOne -= 1;
            }

            // Check Index
            if (inverseWeightTable[w] < inverseWeightTable[subsequence.First()])
            {
                // check element
                if (w < subsequence.First())
                {
                    subsequence.Insert(0, w);
                    success = 0;
                }
            }
            else if (inverseWeightTable[w] > inverseWeightTable[subsequence.Last()])
            {
                //Check element
                if (w > subsequence.Last())
                {
                    subsequence.Add(w);
                    success = 0;
                }
            }
            else if (inverseWeightTable[subsequence[pMinusOne]] > inverseWeightTable[w] && inverseWeightTable[w] > inverseWeightTable[subsequence.First()])
            {
                // Between First and Pivot+1
                if (Pivot > w && w > subsequence.First())
                {
                    InsertLower(subsequence, w, currentPivotIndex);
                    success = 0;
                }

            }
            else if (inverseWeightTable[subsequence[pPlusOne]] < inverseWeightTable[w] && inverseWeightTable[w] < inverseWeightTable[subsequence.Last()])
            {
                // Between Last and Pivot
                if (Pivot < w && w < subsequence.Last())
                {
                    InsertUpper(subsequence, w, currentPivotIndex);
                    success = 0;
                }
            }

            return success;
        }

        public int TryInsert(int a)
        {
            int success = -1;
            int currentPivotIndex = subsequence.IndexOf(Pivot);
            
            int pPlusOne = currentPivotIndex;
            int pMinusOne = currentPivotIndex;

            //if( currentPivotIndex + 1 < subsequence.Count)
            //{
            //    pPlusOne += 1;
            //}

            //if (currentPivotIndex - 1 > 0)
            //{
            //    pMinusOne -= 1;
            //}
            
            // Check Index
            if (inverseTable[a] < inverseTable[subsequence.First()])
            {
                // check element
                if (a < subsequence.First())
                {
                    subsequence.Insert(0, a);
                    success = 0;
                }
            }
            else if (inverseTable[a] > inverseTable[subsequence.Last()])
            {
                //Check element
                if (a > subsequence.Last())
                {
                    subsequence.Add(a);
                    success = 0;
                }
            }
            else if(inverseTable[subsequence[pMinusOne]] > inverseTable[a] &&  inverseTable[a] > inverseTable[subsequence.First()])
            {
                // Between First and Pivot+1
                if (Pivot > a && a > subsequence.First())
                {
                    InsertLower(subsequence, a, currentPivotIndex);
                    success = 0;
                }

            }
            else if (inverseTable[subsequence[pPlusOne]] < inverseTable[a] && inverseTable[a] < inverseTable[subsequence.Last()])
            {
                // Between Last and Pivot
                if (Pivot < a && a < subsequence.Last())
                {
                    InsertUpper(subsequence, a, currentPivotIndex);
                    success = 0;
                }
            }
            
            return success;
        }

        private void InsertLower(List<int> seq, int value, int endIndex)
        {
            for (int i = 0; i < endIndex; i++)
            {
                if (i + 1 <= endIndex)
                {
                    if (seq[i] < value && value < seq[i + 1])
                    {
                        if( inverseTable[seq[i]] < inverseTable[value] && inverseTable[value] < inverseTable[seq[i +1]]) 
                            seq.Insert(i + 1, value);
                    }
                }
            }

        }

        private void InsertUpper(List<int> seq, int value, int startIndex)
        {
            for (int i = startIndex; i < seq.Count; i++)
            {
                if (i + 1 < seq.Count)
                {
                    if (seq[i] < value && value < seq[i + 1])
                    {
                        if (inverseTable[seq[i]] < inverseTable[value] && inverseTable[value] < inverseTable[seq[i + 1]]) 
                            seq.Insert(i + 1, value);
                    }
                }
            }

        }
    }
}
