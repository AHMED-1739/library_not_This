using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Console;
namespace dosomething
{
    class Library
    {
        public List<Book> Books = new List<Book>();
        public Library()
        {
            Books = new List<Book>
            {
            new Book("Origins", "Lewis Dartnell", "History"),
            new Book("1491", "Charles C. Mann", "History"),
            new Book("Sapiens", "Yuval Noah Harari", "History"),
            new Book("Cosmos", "Carl Sagan", "Physics"),
            new Book("Light", "Albert A. Michelson", "Physics"),
            new Book("Quantum", "Manjit Kumar", "Physics"),
            new Book("Animal Farm", "George Orwell", "Novels"),
            new Book("Dracula", "Bram Stoker", "Novels"),
            new Book("Frankenstein", "Mary Shelley", "Novels"),
            new Book("Ethics", "Baruch Spinoza", "Philosophy"),
            new Book("Being", "Martin Heidegger", "Philosophy"),
            new Book("Existence", "Jean-Paul Sartre", "Philosophy"),
            new Book("test1","test1","History"),
            new Book("test1","test1","Novels"),
            new Book("test1","test1","Philosophy"),
            new Book("test1","test1","Uncategorized")
            };
        }
        //search method
        public List<Book> Search(string Title, string Author)
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
                throw new Exception("Author name or book title cannot be blank.");
            List<Book> temp_Books = (from book in Books
                                     where book.Title == Title && book.Author == Author
                                     select book).ToList();
            return temp_Books;
        }
        //We will use this method if the user doesn't know whether what they know is the title or the author's name
        public (List<Book>, List<Book>) SearchByBoth(string TitleOrAuthor)
        {
            if (string.IsNullOrWhiteSpace(TitleOrAuthor))
                throw new Exception("there is no book whose author's name or title are nothing!!!!!");

            List<Book> MatchTheAuthorsName = new List<Book>();
            List<Book> MatchTheTitle = new List<Book>();

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].Title == TitleOrAuthor)
                    MatchTheTitle.Add(Books[i]);
                if (Books[i].Author == TitleOrAuthor)
                    MatchTheAuthorsName.Add(Books[i]);
            }

            return (MatchTheAuthorsName, MatchTheTitle);
        }
        public void Add(Book Added_Book)
        {
            if (string.IsNullOrWhiteSpace(Added_Book.Title) || string.IsNullOrWhiteSpace(Added_Book.Author))
            {
                throw new Exception("the book must have a title and author name");
            }
            for (int i = 0; i < Books.Count; i++)
                if (Books[i].Author == Added_Book.Author && Books[i].Title == Added_Book.Title && Books[i].Subject == Added_Book.Subject)
                {
                    WriteLine("the book is already in the library.");
                    return;
                }
            WriteLine("The book has been added.\n--------------------");
            Books.Add(Added_Book);
        }
        //Show random books to user
        public void DisPlayRandomBook()
        {
            int limit;
            if (Books.Count == 0)
            { WriteLine("there is no book in this library!."); return; }
            else if (Books.Count < 4)
                limit = Books.Count;
            else
                limit = 4;
            Random random = new Random();
            int i = 0;
            while (i < limit)
            {
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                int index = random.Next(0, Books.Count);
                WriteLine("Title: {0}\nAuthor: {1}\nSubject: {2}", Books[index].Title, Books[index].Author, Books[index].Subject);
                ResetColor();
                WriteLine("-----------------------------");
                i++;
            }
        }
        // this fuction displays books for a given topic
        public void Books_Subject(string subject)
        {
            List<Book> temp_Books = (from book in Books where book.Subject == subject select book).ToList();
            Group_Dispaly(temp_Books);
        }
        static public int Available_Book(List<Book> books)
        {
            int Availabele_Books = 0;
            foreach (Book book in books)
                if (book.IsAvailable)
                    Availabele_Books++;
            return Availabele_Books;
        }
        public void Information_Of_Book(Book book)
        {
            string Available_Or_Not = "this book is available";
            if (!book.IsAvailable)
                Available_Or_Not = "this book is not available";
            WriteLine("Title: {0}\nAuthor: {1}\nSubject: {2}\n{3}.", book.Title, book.Author, book.Subject, Available_Or_Not);
        }
        public void Group_Dispaly(List<Book> books)
        {
            foreach (Book temp in books)
            {
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                Information_Of_Book(temp);
                ResetColor();
                WriteLine("============================");
            }
        }
    }
}