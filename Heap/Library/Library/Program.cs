using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Book b1 = new Book("zaglavie", "avtor", "izdatelstvo", 1, 123);
                Book b2 = new Book("zaglavie2", "avtor2", "izdatelstvo2", 2, 456);
                Book b3 = new Book("zaglavie3", "avtor3", "izdatelstvo3", 3, 789);
                List<Book> list = new List<Book>();
                Library lib = new Library("biblioteka", list);
                lib.AddBook(b1);
                lib.AddBook(b2);
                lib.AddBook(b3);
                lib.Remove(b1);
                lib.Search("avtor");
                b1.Information();
                b2.Information();
                b3.Information();
            }
        }
    }
}
