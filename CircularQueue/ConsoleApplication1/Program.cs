using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//set as startUp project

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Stack<int> stack = new Stack<int>();
            Stack<string> demoStack = new Stack<string>();
            

            demoStack.Push("dsadas");
            demoStack.Push("yesbee");

            
            Console.WriteLine(demoStack.Count);



            while (demoStack.Count>0)
            Console.WriteLine(demoStack.Pop());

            Console.WriteLine("\n");

            stack.Push(10);
            stack.Push(30);
            stack.Push(410);

            while (stack.Count > 0)
                Console.WriteLine(stack.Pop());


            Console.WriteLine("\n");
            Console.WriteLine("dasdassad");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");

            Console.WriteLine("tova e veche deto pochva !!! :) ");


            string expression = "1 + (2 - (2+3) * 4 / (3+1)) * 5";
            //pazim indxite

            Stack<int> bracketIndex = new Stack<int>();

            for (int i = 0; i < expression.Count(); i++ )
            {
                char s = expression[i];
                
                 if (s== '(')
                {
                    bracketIndex.Push(i);
                }
                 else
                     if (s==')')
                     {
                         int lastIndex = bracketIndex.Pop();
                         Console.WriteLine(expression.Substring(lastIndex, (i-lastIndex+1)));

                     }

            }








                Console.ReadKey();

        }
    }
}
