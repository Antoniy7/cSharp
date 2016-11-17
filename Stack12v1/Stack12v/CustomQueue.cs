using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack12v
{
    class CustomQueue
    {
        private object[] queue;
        private int count;
        private int capacity;
        private int position;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        public CustomQueue(int size = 4)
        {
            Capacity = size;
            count = 0;
            position = 0;
            queue = new object[Capacity];
        }
        void Resize()
        {
            if (Count + 1 > Capacity)
            {
                Capacity = Capacity * 2;
                object[] temp = new object[Capacity];
                for (int i = 0; i < Count; i++)
                    temp[i] = queue[i];
                queue = temp;
            }
        }
        public void Enqueue(object value)
        {
            Resize();
            queue[Count] = value;
            count++;
        }
        public object Dequeue()
        {
            if (Count > 0)
            {
                count--; 
                return queue[position++];
            }
            throw new Exception("No elements to dequeue");
        }
        public object Peek()
        {
            if(Count>0)
            {
                return queue[position];
            }
            throw new Exception("No elements to peek");
        }
        public void Clear()
        {
            count = 0;
            capacity = 4;
        }
       
    }
}
