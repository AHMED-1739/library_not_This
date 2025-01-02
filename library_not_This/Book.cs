using System;
namespace dosomething
{
    class Book
    {
        public readonly string Title;
        public readonly string Author;
        public readonly string Subject;
        public bool IsAvailable;
        public Book(string Title, string Author, string Subject)
        {
            this.Title = Title;
            this.Author = Author;
            this.Subject = Subject;
            IsAvailable = true;
        }
    }
}