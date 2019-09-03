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
    public class Node<T> : IEnumerable
    {
        private T data;
        private HashSet<Node<T>> children;        

        public Node(T data)
        {
            this.data = data;
            children = new HashSet<Node<T>>();
        }

        /// <summary>
        /// Add child Node
        /// </summary>
        /// <param name="data"></param>
        public void AddChild(T data)
        {
            children.Add(new Node<T>(data));
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

        //public T NodeData
        //{
        //    get
        //    {
        //        return this.Data;
        //    }
        //    set
        //    {
        //        this.Data = value;
        //    }
        //}

        public bool IsChildExist
        {
            get
            {
                return (children.Count > 0);
            }
        }

        public T NodeData { get => data; set => data = value; }

        //public void Traverse(Node<T> node, Action<Node<T>> nodeAction)
        //{
        //    nodeAction.Invoke(node);
        //    foreach (var kidNode in node.children)
        //    {
        //        Traverse(kidNode, nodeAction);
        //    }
        //}

        /// <summary>
        /// Return Number of Children
        /// </summary>
        //public int ChildrenSize
        //{
        //    get
        //    {
        //        return children.Count;
        //    }
        //}

        /// <summary>
        /// Move to the next child of this node starting from the left hand side
        /// </summary>
        /// <returns></returns>
        //public bool MoveNext()
        //{
        //    position++;
        //    return position < children.Count;
        //}

        /// <summary>
        /// Reset back to the first child of this node starting from the left hand side
        /// </summary>
        //public void Reset()
        //{
        //    position = - 1;
        //}

        public IEnumerator GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        /// <summary>
        /// Current Child of this node
        /// </summary>
        //public object Current => this.children.ElementAt(position);

    }
}
