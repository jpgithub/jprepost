using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NumberAnalyzis
{
    class Program
    {
        static void Main(string[] args)
        {
            Analyzer analyzer = new Analyzer();

            // Simulating Event Call Handler Code
            Random input = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            
            int index = 1;
            while (!analyzer.IsComplete)
            {
                //Setup Data 
                Matrix matData = new Matrix();
                matData.Id = index++;
                for (int i = 0; i < matData.Data.Length; i++)
                {
                    matData.Data[i] = (input.Next() % ushort.MaxValue) & 0x3FFF;
                }
                //Feed into filter
                analyzer.FilterNumber(matData);
            }
            Console.Out.WriteLineAsync(analyzer.IsComplete.ToString());
            //To Do:Generate Report .XML File and return output array

            Console.ReadLine();
        }
    }
}
