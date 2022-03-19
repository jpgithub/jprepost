using Graphs;
using Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeExercise
{
    class Program
    {
        private static BSTree bstree;

        public static Trie<T> CreateTrie<T>(T data)
        {
            return new Trie<T>(data);
        }

        private static List<int> sorteddays = new List<int>();
        private static Dictionary<int,int> _sortedDictionary = new Dictionary<int, int>();
        public static int CalculatedMedian(List<int> days,int starting_index, int windows_size)
        {
            int median = -1;

            //// Sort
            if (sorteddays.Count == 0)
            {
                sorteddays = new List<int>(days.GetRange(starting_index,windows_size));
                sorteddays.Sort();
            }
            else
            {
                //// to keep the windows size in check
                int od = days[starting_index - 1];
                int nd = days[starting_index + windows_size - 1];
                int selected_index = -1;
                for (int i = 0; i < sorteddays.Count; i++)
                {
                    if (od == sorteddays[i])
                    {
                        selected_index = i;
                    }

                    if (sorteddays[i] < nd || nd == 0)
                    {
                        if (i + 1 >= sorteddays.Count)
                        {
                            sorteddays.Add(nd);
                            nd = -1;
                        }
                        else
                        {
                            if (sorteddays[i + 1] >= nd)
                            {
                                sorteddays.Insert(i + 1, nd);
                                nd = -1;
                            }
                        }
                    }                    
                }
                sorteddays.RemoveAt(selected_index);
            }
            
            if (windows_size % 2 == 0)
            {
                var h = windows_size / 2;
                var l = (windows_size / 2) - 1;

                median = (sorteddays[h] + sorteddays[l]);
            }
            else
            {
                median = sorteddays[windows_size / 2];
            }
            return median;
        }

        public static int CalculatedMedian(int offset, int windows_size)
        {
            int median = -1;

            //// Calculated Median
            if (windows_size % 2 == 0)
            {
                var h = windows_size / 2;
                var l = (windows_size / 2) - 1;

                median = (sorteddays[h + offset] + sorteddays[l + offset]);
            }
            else
            {
                ///// base 1 offset
                median = sorteddays[(windows_size / 2) + offset];
                median *= 2;
            }
            return median;
        }

        public static int CalculatedMedian(int min, int max, int windows_size)
        {
            int median = -1;

            //// Calculated Median
            int total = 0;
            int prevous_key = -1;
            for (int i = min; i <= max; i++)
            {
                total += _sortedDictionary[i];


                if (_sortedDictionary[i] > 0)
                {
                    if (windows_size % 2 == 0)
                    {
                        var h = windows_size / 2;
                        var l = (windows_size / 2) - 1;

                        if (total > h)
                        {
                            //// h = h + 1
                            //// l = h - 1
                            //// if count is less than 2 then we must look in the previous row
                            median = i;
                            h += 1;
                            l = h - 1;
                            // 5053 - 5000 (l) = 44
                            //// calculated previous key delimiter total 
                            int adjustment = total - _sortedDictionary[i];
                            //if l is within the delimiter then we use previous key else l is within the current - "i" range
                            if (adjustment >= l)
                            {
                                median += prevous_key;
                            }
                            else
                            {
                                median += i;
                            }

                            break;
                        }
                    }
                    else
                    {
                        if (total > (windows_size / 2))
                        {
                            median = i;
                            median *= 2;
                            break;
                        }
                    }
                    prevous_key = i;
                }
            }

            return median;
        }

        static void Main(string[] args)
        {

            
            var ans = squareRoot.getRootDecimal(5, 100);



            var exp = new List<int>() { 2, 3, 4, 2, 3, 0, 8, 6, 5 };
            int[] data = new int[201];

            
            for (int i = 0; i < 200 + 1; i++)
            {
                _sortedDictionary.Add(i, 0);
                //sorteddays.Add(0);
            }

            int windows_size = 4;


            sorteddays = new List<int>(new int[windows_size + 2]);

            int min = int.MaxValue;
            int max = int.MinValue;
            for (int i = 0; i < windows_size; i++)
            {
                int v = exp[i];
                _sortedDictionary[v]++;

                //// Find Min and Max
                if (v > max)
                {
                    max = v;
                }
                if(v < min)
                {
                    min = v;
                }
            }

            int notification_count = 0;
            
            for (int i = windows_size, s = 0; i < exp.Count; i++,s++)
            {
                var median = CalculatedMedian(min,max,windows_size);

                if (median <= exp[i])
                {
                    notification_count++;
                }

                int ev = exp[i];

                //// adjust Min and Max
                if (ev > max)
                {
                    max = ev;
                }
                if (ev < min)
                {
                    min = ev;
                }

                //// add new element
                _sortedDictionary[ev]++;

                //// remove element
                _sortedDictionary[exp[s]]--;
                
            }

            //FraudCounter();

            //QuickSort.Sort(exp);

            //var sa = new SuffixArray("abaab");
            //var sa = new SuffixArray("dbac");
            //var sa = new SuffixArray("abaab");

            //sa.AshtonString(3);
            //AVLTreeEx();
            //BsTreeEx();
            //int ans = SimilarityStrings();
            // 1,8,7,2,4,9,11
            //var swq = new SubweightedSeq();

            ////int[] b = new int[] { 5, 1, 2, 4, 3, 1, 2, 3, 4 };
            ////int[] b = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            ////int[] w = new int[] { 10, 20, 30, 40, 50, 15, 15, 15, 90 };

            //int[] b = new int[] { 1, 2, 3, 4, 1, 2, 3, 4 };
            //int[] w = new int[] { 10, 20, 30, 40, 15, 15, 15, 50 };

            //if (swq.LoadSubweightedSeq(b, w))
            //{
            //    int total;
            //    total = swq.MaxSubweightedSeq;
            //}

            // Mergesort
            //var mergsorter = new MergeSort(new int[] { 6, 5, 3 });
            //mergsorter.Sort();
            //var ans = mergsorter.Result;
        }

        private static void FraudCounter()
        {
            string[] lines = System.IO.File.ReadAllLines(@"DataSet-SortEx.txt");
            var input = Array.ConvertAll(lines[0].Split(' '), Int32.Parse).ToList();
            var data = Array.ConvertAll(lines[1].Split(' '), Int32.Parse).ToList();

            var exp = new List<int>(data); // { 2, 3, 4, 2, 3, 6, 8, 4 };

            int notification_count = 0;
            int d = input[1];

            for (int i = 0; i < d + 1; i++)
            {
                sorteddays.Add(-1);
            }

            //// Counting sort
            for (int i = 0; i < 200 + 1; i++)
            {
                _sortedDictionary.Add(i, 0);
                //sorteddays.Add(-1);
            }

            for (int i = 0; i < d; i++)
            {
                int v = exp[i];
                _sortedDictionary[v]++;
            }

            //// Modify dictionary to carry over previous elements
            for (int i = 0; i < _sortedDictionary.Count - 1; i++)
            {
                var v = _sortedDictionary.ElementAt(i).Value;
                _sortedDictionary[_sortedDictionary.ElementAt(i + 1).Key] += v;
            }

            int offset = int.MaxValue;

            for (int k = 0; k < d; k++)
            {
                int key = exp[k];

                sorteddays[_sortedDictionary[key]] = key;

                if (offset > _sortedDictionary[key])
                {
                    offset = _sortedDictionary[key];
                }

                _sortedDictionary[key]--;
            }

            for (int i = d, j = 0; i < exp.Count; i++)
            {
                var median = CalculatedMedian(offset, d);

                if (median <= exp[i])
                {
                    notification_count++;
                }

                //// restore table
                for (int s = 0; s < d; s++)
                {
                    if (sorteddays[s + offset] != -1)
                    {
                        int key = sorteddays[s + offset];
                        _sortedDictionary[key]++;
                        sorteddays[s + offset] = -1;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int s = 0; s < _sortedDictionary.Count - 1; s++)
                {
                    if (s > exp[i] && s > exp[i - d])
                    {
                        break;
                    }

                    if (s >= exp[i - d])
                    {
                        _sortedDictionary[_sortedDictionary.ElementAt(s).Key] -= 1;
                    }
                    if (s >= exp[i])
                    {
                        _sortedDictionary[_sortedDictionary.ElementAt(s).Key] += 1;
                    }
                }

                offset = int.MaxValue;

                for (int k = 0; k < d; k++)
                {
                    int key = exp[k + i - d + 1];
                    sorteddays[_sortedDictionary[key]] = key;
                    if (offset > _sortedDictionary[key])
                    {
                        offset = _sortedDictionary[key];
                    }
                    _sortedDictionary[key]--;
                }
            }
            
        }

        private static void AVLTreeEx()
        {
            var avltree = new AVLTree(4);

            int[] inputs = new int[] { 8, 10, 12, 2 };

            //// Traverse insertion action
            foreach (int datapoint in inputs)
            {
                avltree.Insertion(datapoint);
            }
                                   
        }

        private static void BsTreeEx()
        {
            bstree = new BSTree(15);

            int[] inputs = new int[] { 8, 24, 5, 12, 19, 28, 2, 6, 10, 25, 11, 9 };

            //// Traverse insertion action
            foreach (int datapoint in inputs)
            {
                bstree.Insertion(bstree.RootNode, datapoint);
            }


            //bstree.Insertion(bstree.RootNode, 26);
            bstree.Insertion(bstree.RootNode, 17);
            bstree.Insertion(bstree.RootNode, 25);
            bstree.Insertion(bstree.RootNode, 29);

            // Deletion of leaf node right side and left
            bstree.Deletion(bstree.RootNode, 2);
            bstree.Deletion(bstree.RootNode, 29);

            // Deletion of node with two childrens
            bstree.Deletion(bstree.RootNode, 15);

            bstree.Insertion(bstree.RootNode, 22);
            bstree.Insertion(bstree.RootNode, 23);
            //bstree.Insertion(bstree.RootNode, 21);

            // Deletion of node with 1 child
            bstree.Deletion(bstree.RootNode, 12);
            bstree.Deletion(bstree.RootNode, 19);

            //bstree.Deletion(bstree.RootNode, 11);
            //bstree.Deletion(bstree.RootNode, 24);

            //bstree.Deletion(bstree.RootNode, 19);

            foreach (int datapoint in inputs)
            {
                var ans = bstree.Find(bstree.RootNode, datapoint);
                Console.WriteLine(string.Format("{0} : {1}", datapoint, ans));
            }
        }

        private static int SimilarityStrings()
        {
            //https://www.hackerrank.com/challenges/string-similarity/problem
            //var letters = "ababaa".ToCharArray();

            var letters = "aa".ToCharArray();

            Trie<string> searchpool = CreateTrie<string>(letters[0].ToString());

            // populated the trie
            int i = 1;
            var currentNode = searchpool.GetRootNode;
            while (i < letters.Length)
            {
                currentNode.AddChild(letters[i].ToString());
                currentNode = currentNode.GetChild(0);
                i++;
            }


            int total = 0;
            i = 0;
            while (i < letters.Length)
            {
                int j = i;
                int count = 0;
                bool skipAll = false;
                searchpool.Traverse(searchpool.GetRootNode, (currentnode) =>
                {
                    if (currentnode.NodeData == letters[j].ToString() && !skipAll)
                    {
                        Console.WriteLine(currentnode.NodeData);
                        count++;
                    }
                    else
                    {
                        // If the first letter don't match then skip all
                        skipAll = true;
                        return;
                    }
                    j++;
                    if (j >= letters.Length)
                    {
                        skipAll = true;
                        j = 0;
                    }
                });
                total += count;
                i++;
            }
            return total;
        }

    }
}
