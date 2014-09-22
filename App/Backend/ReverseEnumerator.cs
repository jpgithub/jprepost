using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Backend
{
    public class ReverseEnumerator:IEnumerator
    {
        //public List<object> list;
        private int position = 0;
        private int begin = 0;

        /// <summary>
        /// Set the interator starting position 
        /// </summary>
        public int Begin
        {
            set
            {
                this.begin = value;
                Reset();
            }
        }
        public object Current
        {
            get 
            {
                return position;
            }
        }

        public bool MoveNext()
        {
            position--;
            return (position >= uint.MinValue);
        }

        /// <summary>
        /// Reset to the beginning position
        /// </summary>
        public void Reset()
        {
            position = begin;
        }

        /// <summary>
        /// Reposition Forward Iterator to current location
        /// </summary>
        public void RepositionIterator(ref IEnumerator forwardIterator, object currentFrame )
        {
            forwardIterator.Reset();
            forwardIterator.MoveNext();
            while (forwardIterator.Current != currentFrame)
            {
                forwardIterator.MoveNext();
            }
        
        }

    }
}
