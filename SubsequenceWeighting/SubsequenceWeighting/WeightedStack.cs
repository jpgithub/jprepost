using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsequenceWeighting
{
    public class WeightedStack
    {
        private Stack<(int, int)> ssubseq = new Stack<(int, int)>();
        private uint weight;
        private (int, int) bottom;
        private bool basecorrection;

        public (int, int) GetBase
        {
            get
            {
                return bottom;
            }
        }

        public void BaseCorrection((int, int) nb)
        {
            basecorrection = true;
            this.bottom = nb;
        }
        public int Count
        {
            get
            {
                return ssubseq.Count;
            }
        }

        public (int, int) Peek()
        {
            return ssubseq.Peek();
        }

        public uint Weight
        {
            get
            {
                return this.weight + (uint)bottom.Item2;
            }
        }

        public int StartingIndex { get; internal set; }

        public void Push((int, int) v)
        {
            //// Key should be increasing while weight should be decreasing
            if (ssubseq.Count == 0)
            {
                ssubseq.Push(v);
                bottom = v;
            }
            else
            {
                var e = ssubseq.Peek();
                if (e.Item1 > v.Item1)
                {
                    ssubseq.Push(v);
                    weight += (uint)v.Item2;
                }
            }

        }

        public (int, int) Pop()
        {
            if (ssubseq.Count == 1)
            {
                //// Check base correction
                if (basecorrection)
                {
                    ssubseq.Pop();
                    return bottom;
                }
            }
            return ssubseq.Pop();
        }
    }
}
