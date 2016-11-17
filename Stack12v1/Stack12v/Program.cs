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
           CustomQueue test = new CustomQueue();
           
            test.Enqueue(20);
            test.Enqueue(40);
            test.Enqueue(50);
            test.Enqueue(12412);


            Console.ReadKey();

        }
    }
}
