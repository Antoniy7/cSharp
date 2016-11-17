using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    class DynamicBinarySearchTree
    {
        class Node
        {
            private int element;
            private Node left, right, parent;
            public int Element
            {
                get { return element; }
                set { element = value; }
            }
            public Node Left
            {
                get { return left; }
                set { left = value; }
            }
            public Node Right
            {
                get { return right; }
                set { right = value; }
            }
            public Node Parent
            {
                get { return parent; }
                set { parent = value; }
            }
            public Node(int item)
            {
                element = item;
                right = left = parent = null;
            }
        }

        private int count;
        private Node root;

        public int Count
        {
            get { return count; }
        }

        public DynamicBinarySearchTree()
        {
            root = null;
            count = 0;
        }

        public void Add(int item)
        {
            count++;
            if (root == null) root = new Node(item);
            else
            {
                Add(root, item);
            }
        }

        private void Add(Node node, int item)
        {
            if(item<=node.Element)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(item);
                    node.Left.Parent = node;
                }
                else Add(node.Left, item);
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node(item);
                    node.Right.Parent = node;
                }
                else Add(node.Right, item);
            }
        }

        public void InOrder()
        {
            InOrder(root);
            Console.WriteLine();
        }

        private void InOrder(Node node)
        {
            if (node!=null)
            {
                InOrder(node.Left);
                Console.Write("{0} ",node.Element);
                InOrder(node.Right);
            }
        }

        public bool Contains(int item)
        {
            return Contains(root, item);
        }

        private bool Contains(Node node, int item)
        {
            if (node == null) return false;
            if (node.Element == item) return true;
            if (item < node.Element) return Contains(node.Left, item);
            else return Contains(node.Right, item);
        }
    }
}
