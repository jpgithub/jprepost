using System;
using System.Threading.Tasks;

namespace CATaskDelayExercise
{
    class Program
    {
        static void Print()
        {
            Console.WriteLine(string.Format("Hello World!,{0} \n Current Time: {1} ", Task.CurrentId, DateTime.UtcNow));
        }


        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Hello World!,{0} \n Current Time: {1} ", Task.CurrentId, DateTime.UtcNow));
            Task.Factory.ContinueWhenAll(new Task[1] { Task.Delay(5000) } , completedTasks =>
            {
                Print();
            }).Wait();
        }
    }
}
