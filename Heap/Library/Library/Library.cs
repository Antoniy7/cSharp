using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    class Book
    {
        private string title;
        private string author;
        private string publishing;
        private int year;
        private int isbn;

        public int ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public string Publishing
        {
            get { return publishing; }
            set { publishing = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public void Information()
        {
            Console.WriteLine("{0} {1} {2} {3} {4}", Title, Author, Publishing, Year.ToString(), ISBN.ToString());
        }

        public Book(string title, string author, string publishing, int year, int isbn)
        {
            Title = title;
            Author = author;
            Publishing = publishing;
            Year = year;
            ISBN = isbn;
        }
    }

    class Library
    {
        private string name;
        private List<Book> books;

        public List<Book> Books
        {
            get { return books; }
            set { books = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }
        public void Search(string author)
        {
            bool find = false;
            for (int i=0 ; i< books.Count; i++)
            {
                if ((books[i].Author).CompareTo(author) == 0)
                 find = true;
            }
            if (find == true)
            {
                Console.WriteLine("Exists");
            }
            else
                Console.WriteLine("Doesn't exist");
            
        }
        public void Remove(Book book)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ISBN == book.ISBN)
                { books.RemoveAt(i) ; }
            }
        }
        public Library(string name, List<Book> books)
        {
            Name = name;
            Books = books;
        }

    }






}
