using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Prefix Tree a.k.a (Trie)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Trie<T>
    {
        public Trie(T data)
        {
            this.GetRootNode = new Node<T>(data);
        }

        public Node<T> GetRootNode { get; protected set; }

        /// <summary>
        /// Traverse the entire tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeAction"></param>
        public void Traverse(Node<T> node, Action<Node<T>> nodeAction)
        {
            nodeAction.Invoke(node);
            foreach (Node<T> kidNode in node)
            {
                Traverse(kidNode, nodeAction);
            }
        }
    }
}
