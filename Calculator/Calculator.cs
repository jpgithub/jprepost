using System;
using System.Runtime.InteropServices;

namespace Calculator
{
    class Calculator
    {
        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void fibonacci_init(ulong a,ulong b);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool fibonacci_next();

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern uint fibonacci_index();

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern ulong fibonacci_current();

        static void Main(string[] args)
        {
            ulong a = 1;
            ulong b = 1;
            fibonacci_init(a,b);
            uint indexans = fibonacci_index();
            while (fibonacci_next())
            {
                ulong currentans = fibonacci_current();
                indexans = fibonacci_index();
                Console.Out.WriteLine(string.Format("Index: {0}", indexans));
                Console.Out.WriteLine(string.Format("Current: {0}", currentans));
            }
            
        }
    }
}
