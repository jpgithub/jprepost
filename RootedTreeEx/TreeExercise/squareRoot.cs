using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeExercise
{
    public class squareRoot
    {
        public static int getRoot(int x)
        {
            int root = 0; 
            // Square Value
            for(int i = 0; i < x; i++)
            {
                if( i * i <= x )
                {
                    root = i;
                }
                else
                {
                    break;
                }
            }
            return root;
        }

        public static long getRootBB(int x)
        {
            if( x == 0  || x == 1 )
            {
                return x;
            }

            long start = 1, end = x, ans = 0;

            while (start <= end)
            {
                long mids = (start + end) / 2;
                // if x is a perfect square
                if (mids * mids == x)
                {
                    return mids;
                }
                else if (mids * mids < x)
                {
                    start = mids + 1;
                    ans = mids;
                }
                else
                {
                    end = mids - 1;
                }
            }
            return ans;
        }

        public static float getRootDecimal(int x, int percision = 1)
        {
            float start = 1, end = x, ans = 0;

            while (start <= end)
            {
                float mids = (start + end) / 2;
                // if x is a perfect square
                if (mids * mids == x)
                {
                    ans = mids;
                    break;
                }
                else if (mids * mids < x)
                {
                    start = mids + (1 / (float)percision);
                    ans = mids;
                }
                else
                {
                    end = mids - (1 / (float)percision);
                }
                
            }
            return ans;
        }
    }
}
