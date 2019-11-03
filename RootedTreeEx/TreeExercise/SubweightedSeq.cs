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
        private List<int> weightlist = new List<int>();
        private int maxweight = 0;

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
                        //Traverse(node, AddChild(b[i], w[i]));
                        CalculatedSubweightSeq(node, b[i], w[i]);
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

        private void CalculatedSubweightSeq(Node<Tuple<int, int>> node, int b, int w, int total = 0)
        {
            if (node.NodeData.Item1 < b)
            {
                node.AddChild(Tuple.Create<int, int>(b, w));
            }

            if (!node.IsChildExist)
            {
                int ans = node.NodeData.Item2 + total;

                if (ans > maxweight)
                {
                    maxweight = ans;
                }
            }
            else
            {
                int weight = node.NodeData.Item2;
                foreach (Node<Tuple<int, int>> kidNode in node)
                {
                    CalculatedSubweightSeq(kidNode, b, w, total + weight);
                }
            }
        }

        public int MaxSubweightedSeq
        {
            get
            {
                return maxweight;
            }
        }

        private void CalculatedSubweightSeq(Node<Tuple<int, int>> root, int total = 0)
        {
            if (!root.IsChildExist)
            {
                int ans = root.NodeData.Item2 + total;

                if(ans > maxweight)
                {
                    maxweight = ans;
                }

                weightlist.Add(ans);
            }
            else
            {
                int weight = root.NodeData.Item2;
                foreach (Node<Tuple<int, int>> kidNode in root)
                {
                   CalculatedSubweightSeq(kidNode,total + weight);
                }
            }
        }

        private static Action<Node<Tuple<int, int>>> AddChild(int b, int w)
        {
            return (currentnode) =>
            {
                // Add Child Condition
                if (currentnode.NodeData.Item1 < b)
                {
                    currentnode.AddChild(Tuple.Create<int, int>(b, w));
                    //return;
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
