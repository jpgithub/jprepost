using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeExercise
{
    class Program
    {
        public static Trie<T> CreateTrie<T>(T data)
        {
            return new Trie<T>(data);
        }

        static void Main(string[] args)
        {
            var letters = "ababaa".ToCharArray();

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
            while(i < letters.Length)
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
                    if(j >= letters.Length)
                    {
                        skipAll = true;
                        j = 0;
                    }
                });
                total += count;                
                i++;
            }
        }

        //private static int MatchCount(Trie<string> searchpool, Node<string> node)
        //{
        //    int count = 0;
        //    searchpool.Traverse(node, (currentnode) =>
        //    {
        //        Console.WriteLine(currentnode.NodeData);
        //        count++;

        //    });
        //    return count;
        //}
    }
}
