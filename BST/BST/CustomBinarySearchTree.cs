using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    class CustomBinarySearchTree<T>
    {
        private T[] arr;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public CustomBinarySearchTree()
        {
            arr = new T[4];
            count = 0;
        }

        public void Add(T item)
        {
            count++;
            Add(1, item);
        }

        private void Add(int index, T item)
        {
            if(index>=arr.Length)
            {
                T[] extendedArr = new T[2 * arr.Length];
                Array.Copy(arr, extendedArr, arr.Length);
                arr = extendedArr;
            }
            if(arr[index].Equals(default(T)))
            {
                arr[index] = item;
            }
            else
            {
                int result = Comparer<T>.Default.Compare(item, arr[index]);
                if (result <= 0) Add(2 * index, item);
                else Add(2 * index + 1, item);
            }
        }

        public void InOrder()
        {
            InOrder(1);
            Console.WriteLine();
        }

        private void InOrder(int index)
        {
            if(2*index<=arr.Length-1 && !arr[2*index].Equals(default(T)))
            {
                InOrder(2 * index);
            }
            Console.Write("{0} ", arr[index]);
            if (2 * index+1 <= arr.Length-1 && !arr[2 * index+1].Equals(default(T)))
            {
                InOrder(2 * index+1);
            }
        }

        public void PreOrder()
        {
            PreOrder(1);
            Console.WriteLine();
        }

        private void PreOrder(int index)
        {
            Console.Write("{0} ", arr[index]);
            if (2 * index <= arr.Length - 1 && !arr[2 * index].Equals(default(T)))
            {
                PreOrder(2 * index);
            }
            if (2 * index + 1 <= arr.Length - 1 && !arr[2 * index + 1].Equals(default(T)))
            {
                PreOrder(2 * index + 1);
            }
        }

        public int Height()
        {
            return Height(1);
        }

        private int Height(int index)
        {
            if (2 * index >= arr.Length) return 1;
            if (2 * index + 1 >= arr.Length) return 1;
            if(arr[2 * index].Equals(default(T)) && arr[2 * index + 1].Equals(default(T)))
                return 1;
            return Math.Max(Height(2 * index), Height(2 * index + 1)) + 1;
        }

        public bool Contains(T item)
        {
            return Contains(1, item);
        }

        private bool Contains(int index, T item)
        {
            if (index >= arr.Length) return false;
            int result = Comparer<T>.Default.Compare(item, arr[index]);
            if (result == 0) return true;
            if (result<0)
            {
                return Contains(2 * index, item);
            }
            else
            {
                return Contains(2 * index + 1, item);
            }

        }

        public void Remove(T item)
        {
            if (count==0)
            {
                throw new InvalidOperationException("No elements");
            }
            if (Contains(item))
            {
                count--;
                Remove(1, item);
            }
            else
            {
                Console.WriteLine("No");
            }
        }

        private void Remove(int index, T item)
        {
            if (index >= arr.Length) return;
            if(arr[index].Equals(default(T))) return;
            int result = Comparer<T>.Default.Compare(item, arr[index]);
            if (result < 0) Remove(2 * index, item);
            else
                if (result > 0) Remove(2 * index + 1, item);
                else
                {
                    if (2 * index >= arr.Length)
                    {
                        arr[index] = default(T);
                        return;
                    }
                    if (2 * index + 1 >= arr.Length) return;

                    if (arr[2 * index].Equals(default(T)) && arr[2 * index + 1].Equals(default(T)))
                    {
                        arr[index] = default(T);
                        return;
                    }
                    if (arr[2 * index].Equals(default(T)) && !arr[2 * index + 1].Equals(default(T)))
                    {
                        arr[index] = arr[2 * index + 1];
                        arr[2 * index + 1] = item;
                        Remove(2 * index + 1, item);
                        return;
                    }
                    if (!arr[2 * index].Equals(default(T)) && arr[2 * index + 1].Equals(default(T)))
                    {
                        arr[index] = arr[2 * index];
                        arr[2 * index] = item;
                        Remove(2 * index, item);
                        return;
                    }
                    if (!arr[2 * index].Equals(default(T)) && !arr[2 * index + 1].Equals(default(T)))
                    {
                        int temp = 2 * index + 1;
                        while (temp < arr.Length && !arr[temp].Equals(default(T)))
                            temp *= 2;
                        temp /= 2;
                        arr[index] = arr[temp];
                        arr[temp] = item;
                        Remove(temp, item);
                        return;
                    }
                }
        }
    }
}
