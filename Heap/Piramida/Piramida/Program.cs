using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piramida
{
    class Program
    {
        static void Main(string[] args)
        {
            Heap test = new Heap();
           
            test.Push(120);
            test.Push(230);
            test.Push(610);
            test.Push(20);
            test.Push(620);


            //test.Push(4);
            //test.Push(50);
            Console.WriteLine(test.Top());
           
            test.Pop();
            
           Console.WriteLine(test.Top());
            
            
            Console.ReadKey();

        }
    }
}
