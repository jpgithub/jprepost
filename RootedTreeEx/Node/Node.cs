using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Node
{
    /// <summary>
    /// This class represent a graph node,
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        private T data;
        private Dictionary<int, Node<T>> children;

        public Node(T data)
        {
            this.data = data;
            children = new Dictionary<int, Node<T>>();
        }

        public void AddChild(T data)
        {
            children.Add(children.Count, new Node<T>(data));
        }

        public Node<T> GetChild(int i)
        {
            Node<T> child = null;
            if (children.TryGetValue(i, out child))
            {
                return children[i];
            }
            //throw new KeyNotFoundException("Child Node Doesn't Exist");
            return child;
        }

        public void Traverse(Node<T> node, Action<Node<T>> nodeAction)
        {
            nodeAction.Invoke(node);
            foreach (var kidNode in node.children)
            {
                Traverse(kidNode.Value, nodeAction);
            }
        }

    }
}
