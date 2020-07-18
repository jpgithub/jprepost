using System;
using System.Runtime.InteropServices;

namespace Calculator
{
    class Calculator
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct matrixdata
        {
            /// <summary>
            /// Is Transposed
            /// Use only Boolean (1 Byte) to match with C/C++ 
            /// </summary>
            public Boolean istransposed;
            /// <summary>
            /// Value
            /// </summary>
            public int value;
        }

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void fibonacci_init(ulong a,ulong b);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool fibonacci_next();

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern uint fibonacci_index();

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern ulong fibonacci_current();

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int equation(string expression);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int matrixopertionA([Out] matrixdata[] amd, int sizeofarray);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int matrixopertion(ref matrixdata amd);

        static void Main(string[] args)
        {
            var retvalue = equation("y=2x+10");
            matrixdata[] ptramd = new matrixdata[5];
            try
            {
                retvalue = matrixopertionA(ptramd,5);
                if (retvalue == 0)
                {
                    var flag = ptramd[0].istransposed;
                    flag = ptramd[1].istransposed;
                }
            }
            catch(Exception e)
            {
                ;
            }

            //ulong a = 0;
            //ulong b = 1;
            //fibonacci_init(a,b);
            //uint indexans = fibonacci_index();
            //while (fibonacci_next())
            //{
            //    ulong currentans = fibonacci_current();
            //    indexans = fibonacci_index();
            //    Console.Out.WriteLine(string.Format("Index: {0}", indexans));
            //    Console.Out.WriteLine(string.Format("Current: {0}", currentans));
            //    if(indexans >= 93)
            //    {
            //        break;
            //    }
            //}            
        }
    }
}
