using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class CountingSort
    {
        private static Dictionary<int, int> _sortedDictionary;

        public static void Sort(List<int> sequence, (int,int) datarange)
        {
            //// inital
            _sortedDictionary = new Dictionary<int, int>();
            List<int> list = new List<int>();
            //list.Add(-1); /// offset to base 1
            for (int i = datarange.Item1; i < datarange.Item2 + 1; i++)
            {
                _sortedDictionary.Add(i, 0);
                list.Add(-1);
            }

            foreach(int i in sequence)
            {
                _sortedDictionary[i]++;
            }

            //// Modify dictionary to carry over previous elements
            for(int i = 0; i < _sortedDictionary.Count - 1; i++)
            {
                var v = _sortedDictionary.ElementAt(i).Value;
                _sortedDictionary[_sortedDictionary.ElementAt(i + 1).Key] += v;
            }

            for (int i = 0; i < sequence.Count; i++)
            {
                int key = sequence[i];
                list[_sortedDictionary[key]] = key;
                _sortedDictionary[key]--;
            }
            sequence.Clear();
            sequence.AddRange(list);
        }
    }
}
