using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSort
    {
        public static void Sort(List<int> sequence)
        {
            Partition(sequence, sequence.Count / 2, 0, sequence.Count - 1);
        }

        private static void Partition(List<int> sequence,int pivot_index, int si, int ei)
        {
            //// Boundary checker
            if(pivot_index < si || pivot_index > ei || pivot_index == si && pivot_index == ei)
            {
                return;
            }


            //// In-place partition
            int pv = sequence[pivot_index];
            int swapindex_a = -1;
            int swapindex_b = -1;
            for(int i = si; i < pivot_index ; i++)
            {
                if (sequence[i] > pv)
                {
                    swapindex_a = i;                    
                }
            }

            for (int j = pivot_index + 1; j <= ei; j++)
            {
                if (sequence[j] < pv)
                {
                    swapindex_b = j;
                }
            }

            if(swapindex_a == -1 && swapindex_b != -1)
            {
                //// move to the left side
                var temp = sequence[swapindex_b];
                sequence.RemoveAt(swapindex_b);
                sequence.Insert(pivot_index - 1, temp);
                pivot_index++; //// index get shifted up
            }
            else if (swapindex_a != -1 && swapindex_b == -1)
            {
                //// move to the right side
                var temp = sequence[swapindex_a];
                sequence.RemoveAt(swapindex_a);
                sequence.Insert(pivot_index,temp);
                pivot_index--; //// index gets shifted down
            }
            else if(swapindex_a != -1 && swapindex_b != -1)
            {
                var temp = sequence[swapindex_a];
                sequence[swapindex_a] = sequence[swapindex_b];
                sequence[swapindex_b] = temp;
            }
            else
            {
                //// nothing to do - already sorted;
                return;
            }

            Partition(sequence, pivot_index / 2, si, pivot_index);
            Partition(sequence, (sequence.Count + pivot_index) / 2, pivot_index + 1 , ei);

        }
    }
}
