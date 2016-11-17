using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack12v
{
    class DynamicStack
    {
        class Node
        {
            private object element;
            private Node next;
            public object Element
            {
                get { return element; }
                set { element = value; }
            }
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }
            public Node()
            {
                element = null;
            }
        }
        private Node head;
        private int count;
        public int Count
        {
            get{return count;}
            set {count = value;}
        }
        public DynamicStack()
        {
            head = null;
            count=0;
        }
        public object Pop()
        {
            Node temp = head;
            head = head.Next;
            count--;
            return temp.Element;
        }
         public object Peek()
        {
            return head.Element;
        }

        public void Push(object value)
        {
            count++;
            if (head==null)
            {
                head = new Node();
                head.Element = value;
                head.Next = null;    
            }
            else
            {
                Node toAdd = new Node();
                toAdd.Element = value;
                toAdd.Next = head;
                head = toAdd;
            }
        }
        public void Clear()
        {
            head = null;
            count = 0;
        }
    }
}
