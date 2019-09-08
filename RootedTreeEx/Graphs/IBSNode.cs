using System.Collections.Generic;

namespace Graphs
{
    internal interface IBSNode<T>
    {
        Node<T> GetRightNode { get; }
        Node<T> GetLeftNode { get; }
        void AddChild(T data, IEqualityComparer<Node<T>> equalityComparer);
        System.Linq.IOrderedEnumerable<Node<T>> OrderByBSRule();
    }
}