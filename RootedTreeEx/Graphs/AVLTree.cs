using System;

namespace Graphs
{
    /// <summary>
    /// (AVL) Georgy Adelson-Velsky and Evgenii Landis' tree
    /// Rule: Difference in height between left and right must always be less than equal to 1
    /// </summary>
    public class AVLTree 
    {
        private BSTree avltree;
        public AVLTree(int data)
        {
            this.avltree = new BSTree(data);
        }

        public void Insertion(int value)
        {
            avltree.Insertion(avltree.RootNode,value);
            // rebalance if nesscary
            Rebalance();
        }

        private void Rebalance()
        {
            avltree.Traverse(avltree.RootNode, (currentNode) =>
             {


             });
        }

        private void LeftRotation(int [] childrens)
        {

        }

        private void RightRotation(int [] childrens)
        {

        }
    }
}
