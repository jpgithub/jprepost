using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication4.DataObject;

namespace WpfApplication4.Misc
{
    public class ATSComparer : IComparer<ATS>
    {
        public int Compare(ATS x, ATS y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're 
                    // equal.  
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y 
                    // is greater.  
                    return -1;
                }
            }
            else
            {
                // If x is not null... 
                // 
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the  
                    // lengths of the two strings. 
                    // 
                    int retval = x.Date.CompareTo(y.Date);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length, 
                        // the longer string is greater. 
                        // 
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length, 
                        // sort them with by Timestamp
                        retval = x.TimeStamp.CompareTo(y.TimeStamp);

                        if (retval != 0)
                        {
                            return retval;
                        }
                        else
                        {

                            // If the strings are of equal length, 
                            // sort them with ordinary string comparison. 
                            //
                            return x.Date.CompareTo(y.Date);
                        }
                        
                    }
                }
            }
        }
    }
}
