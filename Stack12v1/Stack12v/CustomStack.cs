using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack12v
{
    class CustomStack
    {
        private object[] stack;
        private int count;
        private int capacity;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public CustomStack(int size = 4)
        {
            capacity = size;
            count = 0;
            stack = new object[capacity];
        }
        void Resize()
        {
            if (Count + 1 > capacity)
            {
                capacity = capacity * 2;
                object[] temp = new object[capacity];
                for (int i = 0; i < Count; i++)
                    temp[i] = stack[i];
                stack = temp;
            }
        }
        public void Push(object value)
        {
            Resize();
            stack[Count] = value;
            count++;
        }
        public object Pop()
        {
            if (Count > 0)
            {
                count--;
                return stack[Count];
            }
            throw new Exception("No elements to pop");
        }
        public object Peek()
        {
            if (Count > 0)
                return stack[Count - 1];
            throw new Exception("No elements to peek");
        }
        public void Clear()
        {
            count = 0;
            capacity = 4;
        }
    }
}
