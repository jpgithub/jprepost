using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class BubbleSort
    {
        public static void Sort(List<int> sequence)
        {
            for(int i  = 0; i < sequence.Count; i++)
            {
                for (int j = i + 1; j < sequence.Count; j++)
                {
                    if(sequence[i] > sequence[j])
                    {
                        //// swap
                        var temp = sequence[i];
                        sequence[i] = sequence[j];
                        sequence[j] = temp;
                    }
                }
            }
        }
    }
}
