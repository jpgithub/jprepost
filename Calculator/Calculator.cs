using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct matrixobject
        {
            /// <summary>
            /// Is Transposed
            /// Use only Boolean (1 Byte) to match with C/C++ 
            /// </summary>
            public Boolean istransposed;
            /// <summary>
            /// Data 
            /// </summary>
            public int data;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct matrixdata
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] public int[] data;
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
        public static extern int matrixopertionA([Out] matrixobject[] amd, int sizeofarray);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int matrixopertion(ref matrixobject amd);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int matrixdataoperation(ref matrixdata amd, int size = 128);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AsyncComputation(ref Int32 computeset, int index);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetIndexedComputationSetSize(ref Int32 computeset, int index);

        [DllImport(@"E:\Users\JOHN\Documents\GitHub\jprepost\MathLibrary\x64\Debug\MathLibrary.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetIndexedComputationSet(ref Int32 computeset, int [] buffer, int bufferlength, int index);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 WaitForSingleObject(IntPtr handle, uint milliseconds);
        static void Main(string[] args)
        {

            Task.Run(() =>
            {
                //// Get Event Handler - Hanlder type is C++ Window.h
                int computeset = 0xA0A0;
                IntPtr handle = AsyncComputation(ref computeset, 0);
                const uint INFINITE = 0xFFFFFFFF;
                while(true)
                {
                    //// Wait for C++ side to signal
                    Console.Out.WriteLine("C#: Waiting for C++ Computational Thread to Signal...");
                    if (WaitForSingleObject(handle, INFINITE) == 0)
                    {
                        ////Signaled Execute Work Task
                        int size = GetIndexedComputationSetSize(ref computeset, 0);
                        int[] buffer = new int[size];
                        int remainder = GetIndexedComputationSet(ref computeset, buffer, buffer.Length, 0);

                        if(remainder == 0)
                        {
                            Console.Out.WriteLine("C#: Printing Out Data:");
                            foreach (int e in buffer)
                            {
                                Console.Out.Write(string.Format(" {0} ",e));
                            }
                            Console.Out.WriteLine(Console.Out.NewLine);
                            break;
                        }
                        else
                        {
                            Console.Out.WriteLine("There More Data to Read:" + remainder);
                        }
                        /// Do next stuff after signal
                    }
                    
                }



            }).Wait();




            ////var retvalue = equation("y=2x+10");
            //matrixobject[] ptramd = new matrixobject[5];
            //matrixobject ptrsinglemd = new matrixobject();
            //matrixdata md = new matrixdata();
            //try
            //{
            //    //ptrsinglemd.datalength = 10;
            //    //ptrsinglemd.data = new int[ptrsinglemd.datalength];
            //    var retvalue = matrixopertion(ref ptrsinglemd);
            //    if (retvalue == 0)
            //    {
            //        var flag = ptrsinglemd.istransposed;
            //    }

            //    retvalue = matrixopertionA(ptramd,5);
            //    if (retvalue == 0)
            //    {
            //        var flag = ptramd[0].istransposed;
            //        flag = ptramd[1].istransposed;
            //    }

            //    retvalue = matrixdataoperation(ref md);
            //    if (retvalue == 0)
            //    {
            //        var flag = md.data[0];
            //        flag = md.data[1];
            //    }
            //}
            //catch(Exception e)
            //{
            //    ;
            //}

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
