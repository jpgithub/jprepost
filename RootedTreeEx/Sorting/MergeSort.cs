using System;

namespace Sorting
{
    public class MergeSort
    {
        private int[] inputsorted;
        private int[] unsortedinput;

        public MergeSort(int[] input)
        {
            this.inputsorted = input;
            this.unsortedinput = input;
        }
        
        public void Sort()
        {
            int startIndex = 0;
            int endingIndex = inputsorted.Length - 1;

            //int[] A = new int[inputsorted.Length];
            //Array.Copy(inputsorted, A, inputsorted.Length);
            PartitionMerge(inputsorted, startIndex, endingIndex);

        }

        public int[] Result
        {
            get
            {
                return inputsorted;
            }
        }

        /// <summary>
        /// Merge function
        /// </summary>
        /// <param name="A">unsorted  input array</param>
        /// <param name="LS"> Left Start </param>
        /// <param name="LE"> Left End </param>
        /// <param name="RS"> Right Start</param>
        /// <param name="RE"> Right End</param>
        private void Merge(int[] A, int LS, int LE, int RS, int RE)
        {
            int[] B = new int[A.Length];
            int leftEnd = LE;
            int startIndex = LS;
            int rightEnd = RE;
            int size = RE - LS + 1;
            
            int lsIndex = LS;
            int rsIndex = RS;
            // index to save element to "B" array
            int index = LS;

            while (lsIndex <= leftEnd && rsIndex <= rightEnd && index < B.Length)
            {
                // Is right element greater than or equal left
                if (A[lsIndex] <= A[rsIndex])
                {
                    B[index] = A[lsIndex];
                    lsIndex++;
                }
                else
                {
                    B[index] = A[rsIndex];
                    rsIndex++;
                }
                index++;
            }

            //Copy A to B, starting from leftStart Index for "A" and "B" 's index
            // Number of elements to copy = LE - LeftStart Index + 1
            Array.Copy(A, lsIndex, B, index, leftEnd - lsIndex + 1);

            //Copy A to B, stating from rightStart Index for "A" and "B" 's index
            // Number of elements to copy = LE - LeftStart Index + 1
            Array.Copy(A, rsIndex, B, index, rightEnd - rsIndex + 1);

            // Copy B to A starting at startIndex with size
            Array.Copy(B, startIndex, A, startIndex, size);
        }
        
        private void Merge(int [] A, int startIndex, int endIndex, int [] B)
        {
            int leftEnd = (endIndex + startIndex) / 2;
            int rightStart = leftEnd + 1;
            int size = endIndex - startIndex + 1;


            int left = startIndex;
            int right = rightStart;
            int index = startIndex;

            while(left <= leftEnd && right <= endIndex && index < B.Length)
            {
                // Is right element greater than or equal left
                if(A[left] <= A[right])
                {
                    B[index] = A[left];
                    left++;
                }
                else
                {
                    B[index] = A[right];
                    right++;
                }
                index++;
            }

            Array.Copy(A, left, B, index, leftEnd - left + 1);
            Array.Copy(A, right, B, index, endIndex - right + 1);
            Array.Copy(B, startIndex, A, startIndex, size);
        }

        private void PartitionMerge(int [] A,int startIndex, int endIndex)
        {
            // single element
            if (startIndex >= endIndex)
            {
                return;
            }

            //divide the indexes into two partiton recursively
            int middleIndex = (endIndex + startIndex) / 2;

            // left half
            // left start (LS) = starting index
            // left end (LE) = to middle index
            PartitionMerge(A, startIndex, middleIndex);
            // right half
            // right start (RS) = middle index + 1 ( to deal with even or odd size input array (A))
            // right end (RE) = end index
            PartitionMerge(A, middleIndex + 1, endIndex);
            // Merge
            //var temp = new int[unsortedinput.Length];
            //Merge(A, startIndex, endIndex, temp);
            Merge(A, startIndex, middleIndex, middleIndex + 1, endIndex);
        }
    }
}
