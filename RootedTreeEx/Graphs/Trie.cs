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
        private readonly Node<T> rootNode;
        

        public Trie(T data)
        {
            this.rootNode = new Node<T>(data);
        }

        public Node<T> GetRootNode
        {
            get
            {
                return rootNode;
            }
        }

        ///// <summary>
        ///// Add Child Node to Root Node
        ///// </summary>
        ///// <param name="data"></param>
        //public void AddChildNode(T data)
        //{
        //    this.rootNode.AddChild(data);
        //}

        ///// <summary>
        ///// Remove Child Node from Root Node
        ///// </summary>
        ///// <param name="data"></param>
        //public void RemoveChildNode(Node<T> data)
        //{
        //    this.rootNode.RemoveChild(data);
        //}

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
