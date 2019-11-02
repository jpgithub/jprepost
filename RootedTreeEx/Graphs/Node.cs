using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// This class represent a graph node,
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> : IEnumerable, IBSNode<T>
    {
        //private T data;
        private HashSet<Node<T>> children;
        private int depth = 0;

        public Node(T data, int level = 0)
        {
            NodeData = data;
            children = new HashSet<Node<T>>();
            depth = level;
        }
        
        public Node(T data, IEqualityComparer<Node<T>> equalityComparer)
        {
            NodeData = data;
            children = new HashSet<Node<T>>(equalityComparer);
        }
        
        /// <summary>
        /// Add child Node by value
        /// </summary>
        /// <param name="data"></param>
        public void AddChild(T data)
        {
            children.Add(new Node<T>(data, depth + 1));
        }


        /// <summary>
        /// Add child Node by reference
        /// </summary>
        /// <param name="data"></param>
        public void AddChild(Node<T> data)
        {
            children.Add(data);
        }

        /// <summary>
        /// Remove child Node
        /// </summary>
        /// <param name="data"></param>
        public void RemoveChild(Node<T> data)
        {
            children.Remove(data);
        }

        /// <summary>
        /// Get a child node using index
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Node<T> GetChild(int i)
        {
            return children.ElementAt(i);
        }

        /// <summary>
        /// Check if this node has any child
        /// </summary>
        public bool IsChildExist
        {
            get
            {
                return (children.Count > 0);
            }
        }

        public T NodeData { get; set; }

        public Node<T> GetRightNode
        {
            get
            {
                if (children.Count == 1)
                {
                    return GetChild(0);
                }
                else
                {
                    return OrderByBSRule().ElementAtOrDefault(1);
                }
            }
        }

        public Node<T> GetLeftNode
        {
            get
            {
                return OrderByBSRule().ElementAtOrDefault(0);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        public IOrderedEnumerable<Node<T>> OrderByBSRule()
        {
            return children.OrderBy(c => c.NodeData);
        }

        /// <summary>
        /// Add a Child to this node. [Max of Two children]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="equalityComparer"></param>
        public void AddChild(T data, IEqualityComparer<Node<T>> equalityComparer)
        {
            if (children.Count < 2)
            {
                children.Add(new Node<T>(data, equalityComparer));
            }
            else
            {
                throw new ArgumentException("Execeed BSTree Child Limit of Two");
            }
        }
    }
}
