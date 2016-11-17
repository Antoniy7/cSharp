using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack12v
{
    class Program
    {
        static void Main(string[] args)
        {

            DynamicStack temp = new DynamicStack();
            
            temp.Push(420);
            temp.Push(520);
            temp.Push(2120);
            temp.Push(24120);

            object neshto = temp.Pop();
            Console.WriteLine(neshto);
          


                Console.ReadKey();

        }
    }
}
