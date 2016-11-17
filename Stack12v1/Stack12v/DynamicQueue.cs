using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack12v
{
    class DynamicQueue
    {
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        class Node
        {
           private Node next;
           private object element;

           public Node Next
           {
               set { next = value; }
               get { return next ;}
           }
            public object Element
           {
               set { element = value; }
               get { return element; }
           }
            public Node()
            {
                next = null;
                element = null;
            }
        }
        Node head;

        public DynamicQueue()
        {
            head = null;
            count = 0;
        }
         public void Enqueue(object value)
        {
            count++;
             if (head==null)
             {
                 head = new Node();
                 head.Element = value;
             }
             else
             {
                 Node temp =head;
                 while(temp.Next!=null)
                 {
                     temp=temp.Next;
                 }
                 temp.Next = new Node();
                 temp.Next.Element = value;
             }
        }
         public object Peek()
         {
             if (Count > 0)
                 return head.Element;
             else
                 throw new Exception("No, elements to peek!");
         }

        public object Dequeue()
         {
             if (Count < 0)
                 throw new Exception("No, elements to dequeue!");
             else
             {
                 count--;
                 Node temp = head;
                 head = head.Next;
                 return temp.Element;
             }
         }
        public void Clear()
        {
            head = null;
            Count = 0;
        }

    }
}
