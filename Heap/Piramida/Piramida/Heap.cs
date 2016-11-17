using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piramida
{
    class Heap
    {
        private int[] arr;
        private int capacity;
        private int count;

        public int Count
        {   
            get { return count; }
            set { count = value; }
        }

        public Heap()
        {
            Count = 0;
            capacity = 4;
            arr = new int[capacity];
        }

        private void Resize()
        {
            if(Count+1>capacity)
            {
                int[] temp = new int[capacity * 2];
                capacity = capacity * 2;
                for (int i = 0; i <Count; i++)
                    temp[i] = arr[i];

                arr = temp;
            }
        }
        
      public void Push(int newValue)
      {
          Resize();
          arr[Count] = newValue;
          bubbleUp(Count++);
      }
      private void bubbleUp(int index)
      {
          if(index == 0)
            return;
          int parentIndex = (index-1)/2;

          if(arr[parentIndex] < arr[index])
          {
            int temp = arr[parentIndex];
            arr[parentIndex] = arr[index];
            arr[index] = temp;
            bubbleUp(parentIndex);
          }

      }

      private void bubbleDown(int index)
      {
          if ((2*index)>Count || ((2*index)+1)>Count)
              return;
          int child1 = index * 2;
          int child2 = index * 2 + 1;

          int bigger;
          if (arr[child1] > arr[child2])
              bigger = child1;
          else
              bigger = child2;

          if (arr[bigger] > arr[index])
          {
              int temp = arr[bigger];
              arr[bigger] = arr[index];
              arr[index] = temp;
          }
          bubbleDown(bigger);
      }


     public int Pop()
      {
          int temp = arr[0];
          arr[0] = arr[--Count];
          bubbleDown(0); 
          return temp;

      }
      public int Top()
      {
          return arr[0];
      }

    }
}
