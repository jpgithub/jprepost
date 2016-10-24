using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberAnalyzis
{
    internal class Analyzer : IFilter
    {
        Queue<Matrix> EvenNumberMTen = new Queue<Matrix>();
        Queue<Matrix> OddNumberMNine = new Queue<Matrix>();

        List<ushort> MatrixMap = new List<ushort>();
        List<ushort> MatrixPattern = new List<ushort>();

        AnalysisReport EvenNumbereport;
        AnalysisReport OddNumbereport;

        bool goodEvenNumber;
        bool goodOddNumber;

        Task firstTask;
        Task secondTask;
        Task allOtherTask;

        public Analyzer()
        {
            firstTask = new Task(OddNumberExecuteAnalysis());
            secondTask = new Task(EvenNumberExecuteAnalysis());
            allOtherTask = new Task(SpecialExecuteAnalysis());
            IsComplete = false;
        }

        /// <summary>
        /// The Analysis Status
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Check the number
        /// </summary>
        /// <param name="number">input number</param>
        public void FilterNumber(Matrix matData)
        {
            if (IsComplete)
                return;

            // Select Mutiple of 10
            if (matData.Id % 2 == 0 && matData.Id % 6 == 0 && !secondTask.IsCompleted)
            {
                this.PrepareEvenNumberMTenAnalysis(matData);
            }
            else if (matData.Id % 2 == 1 && matData.Id % 5 == 0 && !firstTask.IsCompleted)
            {
                this.PrepareOddNumberMNineAnalysis(matData);
            }
            else
            {
                // Wait until First Task and Second Task is done
                if (goodEvenNumber && goodOddNumber && allOtherTask.Status == TaskStatus.Created)
                {
                    //IsComplete = true;
                    allOtherTask.Start();
                }
            }
        }

        /// <summary>
        /// Filter out even number multiple ten and save to filtered queue
        /// </summary>
        /// <param name="number"></param>
        internal void PrepareEvenNumberMTenAnalysis(Matrix matData, int miniThreshold = 36)
        {
            EvenNumberMTen.Enqueue(matData);
            if (secondTask.Status == TaskStatus.Created)
            {
                secondTask.Start();
            }
        }

        /// <summary>
        /// Filter out odd number multiple 9 and save to filtered queue
        /// </summary>
        /// <param name="number"></param>
        internal void PrepareOddNumberMNineAnalysis(Matrix matData, int miniThreshold = 36)
        {
            OddNumberMNine.Enqueue(matData);
            if (firstTask.Status == TaskStatus.Created)
            {
                firstTask.Start();
            }

        }

        /// <summary>
        /// Save Even Data
        /// </summary>
        private int EvenNumberTotal { get; set; }

        /// <summary>
        /// Save Odd Data
        /// </summary>
        private int OddNumberTotal { get; set; }

        /// <summary>
        /// Preform Analysis on Odd Number Task
        /// </summary>
        /// <returns></returns>
        private Action OddNumberExecuteAnalysis()
        {
            return () =>
            {
                OddNumbereport = new AnalysisReport();
                // To Do Setup Default variable
                int index = 0;
                // Perform Calculation and Save
                while (true)
                {
                    if (OddNumberMNine.Count > 0)
                    {
                        OddNumbereport.testMatrixList.Add(OddNumberMNine.Dequeue());
                    }

                    // if (x > miniThreshold)
                    if (OddNumbereport.testMatrixList.Count > 34)
                    {
                        foreach (Matrix testMat in OddNumbereport.testMatrixList)
                        {
                            var avg = 0d;
                            double spTotal = 0d;
                            ++index;
                            if (index < 2)
                            {
                                continue;
                            }
                            // Average
                            avg = testMat.Data.ToList().Average();

                            // Deviation (Spatial)
                            for (int i = 0; i < testMat.Data.Length; i++)
                            {
                                spTotal += Math.Abs(avg - (double)testMat.Data[i]);
                            }

                            spTotal = spTotal / (double)(testMat.Data.Length - 1);

                            OddNumbereport.spList.Add(spTotal);
                            OddNumbereport.meansList.Add(avg);

                            if (index == 34)
                            {
                                break;
                            }
                        }

                        //Drop first two
                        OddNumbereport.testMatrixList.RemoveAt(0);
                        OddNumbereport.testMatrixList.RemoveAt(0);
                        //To Do: Save Data (Async) 
                        goodOddNumber = true;
                        break;
                    }
                }
            };
        }

        /// <summary>
        /// Preform Analysis on Even Number Task
        /// </summary>
        /// <returns></returns>
        private Action EvenNumberExecuteAnalysis()
        {
            return () =>
            {
                EvenNumbereport = new AnalysisReport();
                int index = 0;
                // Perform Calculation and Save 
                while (true)
                {
                    if (EvenNumberMTen.Count > 0)
                    {
                        EvenNumbereport.testMatrixList.Add(EvenNumberMTen.Dequeue());
                    }

                    if (EvenNumbereport.testMatrixList.Count > 34)
                    {
                        foreach (Matrix testMat in EvenNumbereport.testMatrixList)
                        {
                            var avg = 0d;
                            double spTotal = 0d;
                            ++index;
                            if (index < 2)
                            {
                                continue;
                            }

                            // Average
                            avg = testMat.Data.ToList().Average();
                            // Deviation (Spatial)
                            for (int i = 0; i < testMat.Data.Length; i++)
                            {
                                spTotal += Math.Abs(avg - testMat.Data[i]);
                            }
                            spTotal = spTotal / (double)(testMat.Data.Length - 1);

                            EvenNumbereport.spList.Add(spTotal);
                            EvenNumbereport.meansList.Add(avg);
                            if (index == 34)
                            {
                                break;
                            }
                        }

                        EvenNumbereport.testMatrixList.RemoveAt(0);
                        EvenNumbereport.testMatrixList.RemoveAt(0);

                        //To Do: Save Data (Async) 
                        goodEvenNumber = true;
                        break;
                    }
                }
            };
        }

        /// <summary>
        /// Perform Special Calculation
        /// </summary>
        /// <returns></returns>
        private Action SpecialExecuteAnalysis()
        {
            return () =>
            {
                Console.Out.WriteLineAsync("Odd Number\n");
                
                for (int i = 0; i < OddNumbereport.meansList.Count; i++)
                {
                    Console.Out.WriteLineAsync(OddNumbereport.meansList[i].ToString());
                    Console.Out.WriteLineAsync(OddNumbereport.spList[i].ToString());
                }

                Console.Out.WriteLineAsync("Even Number\n");
                
                for (int i = 0; i < EvenNumbereport.meansList.Count; i++)
                {
                    Console.Out.WriteLineAsync(EvenNumbereport.meansList[i].ToString());
                    Console.Out.WriteLineAsync(EvenNumbereport.spList[i].ToString());
                }
                        
                //long grandTotal = EvenNumberTotal + OddNumberTotal;
                IsComplete = true;
            };
        }
    }
}
