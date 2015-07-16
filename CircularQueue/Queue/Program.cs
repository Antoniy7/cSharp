using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {

            Queue<string> q = new Queue<string>();

            q.Enqueue("dsa");
            q.Enqueue("asd");
            q.Enqueue("deiba");

           ///

           // while (q.Count > 0)
                //Console.WriteLine(q.Dequeue());
            // Console.WriteLine(q.Peek()); // proverka dalli e emmpty :) 


            Console.WriteLine("sega vuvejdai chisla");
            //Console.WriteLine(q.Peek());
            //Console.ReadKey();


           

            // It's fine to use foreach...
            foreach (var x in q)
            {
                Console.WriteLine(x);
            }

            /*
            int n = int.Parse(Console.ReadLine());
            //int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());

            Queue<int> sequence = new Queue<int>();
            sequence.Enqueue(n);  // dobavq  

            int index = 0;
            while(sequence.Count>0)
            {
                int currentVar = sequence.Dequeue(); // premahva
                if (currentVar == p)
                {
                    Console.WriteLine(index);//current index
                    break;
                }
                else
                {
                    sequence.Enqueue(currentVar + 1);
                    sequence.Enqueue(currentVar * 2);
                }
                index++;
            }

            */

            Console.ReadKey();


        }
    }
}
