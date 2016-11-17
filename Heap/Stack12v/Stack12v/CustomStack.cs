using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack12v
{
    
    class CustomStack
    {
        private int count;
        private object[] stack;
        private int capacity;
        public int Count 
        {
           get{return count;}
           set{count = value;}
        }

        public int Capacity
        {
            get{return capacity;}
            set{capacity = value;}
        }

        
      public  CustomStack(int razmer=4)
        {
            Capacity = razmer;
            count = 0;
            stack = new object[Capacity];
        }

        void Resize()
        {
            if (Count+1>Capacity)
            {
                Capacity=Capacity*2;
                object[] temp = new object[Capacity];
                for (int i = 0; i < Count; i++)
                    temp[i] = stack[i];
                
                stack = temp;
            }

        }

        public void Push(object value)
        {
            Resize();
            stack[Count] = value;
            count = Count + 1;
            //size = Count;
        }
        public object Pop()
        {
            if (Count > 0)
            {
                count--;
                return stack[Count];
            }

            return null;

        }
        public object Peek()
        {
            if (Count>0)
            return stack[Count-1];
            return null;
        }

        public void clear()
        {
            count = 0;
            capacity = 4;
        }

    }
}
