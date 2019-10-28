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


        static void Main(string[] args)
        {
            //var sa = new SuffixArray("abaab");
            //var sa = new SuffixArray("dbac");
            //var sa = new SuffixArray("abaab");

            //sa.AshtonString(3);
            //AVLTreeEx();
            //BsTreeEx();
            //int ans = SimilarityStrings();
            // 1,8,7,2,4,9,11
            var swq = new SubweightedSeq();

            int[] b = new int[] { 5, 1, 2, 4, 3, 1, 2, 3, 4 };
            int[] w = new int[] { 10, 20, 30, 40, 50, 15, 15, 15, 90 };

            if (swq.LoadSubweightedSeq(b, w))
            {
                ;
            }

            // Mergesort
            //var mergsorter = new MergeSort(new int[] { 6, 5, 3 });
            //mergsorter.Sort();
            //var ans = mergsorter.Result;
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
