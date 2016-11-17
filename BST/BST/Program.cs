using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    class Program
    {
        static void Main(string[] args)
        {
            //CustomBinarySearchTree<int> b = new CustomBinarySearchTree<int>();
            DynamicBinarySearchTree b = new DynamicBinarySearchTree();
            b.Add(15);
            b.Add(20);
            b.Add(10);
            b.Add(20);
            b.Add(12);
            b.Add(3);
            b.Add(18);
            b.Add(2);
            b.Add(30);
            b.Add(13);
            b.Add(7);
            b.Add(11);
            b.InOrder();
         //   Console.WriteLine(b.Count);
            /*
            b.PreOrder();
            Console.WriteLine(b.Height());
            Console.WriteLine(b.Contains(14));
            b.Remove(20);
            b.InOrder();
             */
            Console.ReadKey();
        }
    }
}
