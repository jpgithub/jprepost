using System;
using System.Collections.Generic;

namespace Sorting
{
    public static class InsertionSort
    {
        public static void Sort(List<int> sequence)
        {
			
            //// Ascending order
            for(int i = 0; i < sequence.Count; i++)
            {
                var temp = sequence[i];
                int selectedindex = -1;
                for (int j = i - 1; j >= 0; j--)
                {
                    if(sequence[j] > temp)
                    {
                        selectedindex = j;
                        //sequence.RemoveAt(i);
                        //sequence.Insert(0,temp);
                    }
                }

                if(selectedindex >= 0)
                {
                    sequence.RemoveAt(i);
                    sequence.Insert(selectedindex,temp);
                }

            }
        }
	}
}
