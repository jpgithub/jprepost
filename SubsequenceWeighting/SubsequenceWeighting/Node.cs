using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsequenceWeighting
{
    public class WeightNode
    {
        private List<Tuple<int, int>> children = new List<Tuple<int, int>>();
                
        public List<Tuple<int,int>> Childrens
        {
            get
            {
                return this.children;
            }
        }

        public long GetWeight(int index)
        {
            long ww = 0;
            // find the appropriate weight
            foreach (var w in Childrens)
            {
                if (w.Item1 <= index)
                {
                    ww = w.Item2;
                }
            }

            return ww;
        }
    }
}
