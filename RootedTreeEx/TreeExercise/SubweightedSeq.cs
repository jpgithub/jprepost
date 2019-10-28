using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeExercise
{
    /// <summary>
    /// Subweighted Sequence Exercise
    /// </summary>
    public class SubweightedSeq
    {
        private List<Node<Tuple<int,int>>> treelist = new List<Node<Tuple<int, int>>>();
        
        public bool LoadSubweightedSeq(int [] b,int [] w)
        {
            if(b.Length != w.Length)
            {
                return false;
            }

            for (int i = 0; i < b.Length; i++)
            {
                bool enableAdd = true;
                foreach(var node in treelist)
                {
                    int rootb = node.NodeData.Item1;
                    //int rootweight = node.NodeData.Item2;

                    if(rootb < b[i])
                    {
                        Traverse(node, AddChild(b, w, i));
                        enableAdd = false;
                        // new node add
                    }
                }

                if (treelist.Count == 0 || enableAdd)
                {
                    treelist.Add(new Node<Tuple<int, int>>(Tuple.Create<int, int>(b[i], w[i])));
                    continue;
                }
                
            }
            return true;
        }

        private static Action<Node<Tuple<int, int>>> AddChild(int[] b, int[] w, int i)
        {
            return (currentnode) =>
            {
                // Add Child Condition
                if (currentnode.NodeData.Item1 < b[i])
                {
                    currentnode.AddChild(Tuple.Create<int, int>(b[i], w[i]));
                    return;
                }
            };
        }

        /// <summary>
        /// Traverse the entire tree, preorder fashion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeAction"></param>
        private void Traverse(Node<Tuple<int, int>> node, Action<Node<Tuple<int, int>>> nodeAction)
        {
            nodeAction.Invoke(node);
            foreach (Node<Tuple<int, int>> kidNode in node)
            {
                Traverse(kidNode, nodeAction);
            }
        }
    }
}
