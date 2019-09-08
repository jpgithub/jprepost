using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Binary Search Tree
    /// </summary>
    public class BSTree : IEqualityComparer<Node<int>>
    {
        public BSTree(int data)
        {
            RootNode = new Node<int>(data, this);
        }
        
        public Node<int> RootNode { get; set; }

        public void Insertion(Node<int> currentnode, int data)
        {
            if (currentnode.NodeData > data)
            {
                if (currentnode.GetLeftNode != null && currentnode.GetLeftNode.NodeData < currentnode.NodeData)
                {
                    Insertion(currentnode.GetLeftNode, data);
                }
                else
                {
                    currentnode.AddChild(data, this);
                }
            }

            if (currentnode.NodeData < data)
            {
                if (currentnode.GetRightNode != null && currentnode.GetRightNode.NodeData > currentnode.NodeData)
                {
                    Insertion(currentnode.GetRightNode, data);
                }
                else
                {
                    currentnode.AddChild(data, this);
                }
            }
        }

        public bool Find(Node<int> currentnode, int value)
        {
            bool status = false;

            Traverse(currentnode, (matchNode) =>
            {
                if (matchNode.NodeData == value)
                {
                    status = true;
                }
            });
            return status;
        }

        public void Deletion(Node<int> currentnode, int value)
        {
            Traverse(currentnode, (dnode) =>
            {
                var lftChild = dnode.GetLeftNode;
                var rhgtChild = dnode.GetRightNode;

                // leaf cases
                if (lftChild != null && lftChild.NodeData == value && !lftChild.IsChildExist)
                {
                    dnode.RemoveChild(dnode.GetLeftNode);
                    return;
                }

                if (rhgtChild != null && rhgtChild.NodeData == value && !rhgtChild.IsChildExist)
                {
                    dnode.RemoveChild(dnode.GetRightNode);
                    return;
                }

                // 2 childrens
                if (dnode.NodeData == value && (lftChild.NodeData != rhgtChild.NodeData))
                {
                    // made sure it not a leaf node
                    int min = Minimum(rhgtChild);
                    Deletion(dnode.GetRightNode, min);
                    dnode.NodeData = min;
                    return;
                }

                //1 child
                if (dnode.NodeData == value && (lftChild.NodeData == rhgtChild.NodeData))
                {
                    if (dnode.NodeData > lftChild.NodeData)
                    {
                        dnode.RemoveChild(lftChild);
                        dnode.AddChild(lftChild.GetLeftNode);
                        if (lftChild.GetLeftNode != lftChild.GetRightNode)
                        {
                            dnode.AddChild(lftChild.GetRightNode);
                        }
                        dnode.NodeData = lftChild.NodeData;
                    }
                    else if (dnode.NodeData < rhgtChild.NodeData)
                    {
                        dnode.RemoveChild(rhgtChild);
                        if (rhgtChild.GetLeftNode != rhgtChild.GetRightNode)
                        {
                            dnode.AddChild(rhgtChild.GetLeftNode);
                        }
                        dnode.AddChild(rhgtChild.GetRightNode);
                        dnode.NodeData = rhgtChild.NodeData;
                    }
                    return;
                }
                
            });
        }

        public int Minimum(Node<int> startingNode)
        {
            int min = int.MaxValue;
            Traverse(startingNode, (minNode) =>
            {
                if (minNode.NodeData < min)
                {
                    min = minNode.NodeData;
                }
            });
            return min;
        }

        public void Maximum(Node<int> startingNode)
        {
            int min = -1;
            Traverse(startingNode, (minNode) =>
            {
                if (minNode.NodeData < min)
                {
                    min = minNode.NodeData;
                }
            });
        }


        /// <summary>
        /// Traverse the entire tree, preorder fashion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeAction"></param>
        public void Traverse(Node<int> node, Action<Node<int>> nodeAction)
        {
            nodeAction.Invoke(node);
            foreach (Node<int> kidNode in node.OrderByBSRule())
            {
                Traverse(kidNode, nodeAction);
            }
        }

        /// <summary>
        /// Traverse the entire tree, inorder fashion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeAction"></param>
        public void TraverseInOrder(Node<int> node, Action<Node<int>> nodeAction)
        {
            foreach (Node<int> kidNode in node.OrderByBSRule())
            {
                nodeAction.Invoke(kidNode);
                TraverseInOrder(kidNode, nodeAction);
                nodeAction.Invoke(node);
            }
            
        }

        /// <summary>
        /// Traverse the entire tree, inorder fashion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeAction"></param>
        public void TraversePostorder(Node<int> node, Action<Node<int>> nodeAction)
        {
            foreach (Node<int> kidNode in node.OrderByBSRule())
            {
                nodeAction.Invoke(kidNode);
                TraverseInOrder(kidNode, nodeAction);
            }
            nodeAction.Invoke(node);
        }

        public bool Equals(Node<int> x, Node<int> y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.NodeData == y.NodeData)
                return true;
            else
                return false;
        }

        public int GetHashCode(Node<int> obj)
        {
            int hCode = obj.NodeData;
            return hCode.GetHashCode();
        }
    }
}
