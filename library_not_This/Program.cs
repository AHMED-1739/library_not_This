using library;
using System;
using System.Collections.Generic;
using static System.Console;
namespace dosomething
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Start_Menu_Option = { "Search", "Add", "Exit" };
            string[] Search_Menu_Option = { "Title & Author", "Title OR Author", "Subject", "Random books?" };
            string[] Subject_Menu_Option = { "History", "Physics", "Novels", "Philosophy", "Uncategorized", "Back" };
            Menu Search_Menu = new Menu(Search_Menu_Option, "ESC.\n----Search----");
            Menu Subject_Menu = new Menu(Subject_Menu_Option, "ESC.\n-----Subject-----");
            Menu Start_Menu = new Menu(Start_Menu_Option, @"
        ██╗     ██╗██████╗ ██████╗  █████╗ ██████╗ ██╗   ██╗
        ██║     ██║██╔══██╗██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝
        ██║     ██║██████╦╝██████╔╝███████║██████╔╝ ╚████╔╝
        ██║     ██║██╔══██╗██╔══██╗██╔══██║██╔══██╗  ╚██╔╝
        ███████╗██║██████╦╝██║  ██║██║  ██║██║  ██║   ██║
        ╚══════╝╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝
=====================================================================
(Use the arrow keys to cycle through option and press enter to select.)");

            Library library = new Library();
            bool Check = true;
            while (Check)
            {
                int Start_Menu_Index = Start_Menu.Run();
                //Search
                if (Start_Menu_Index == 0)
                {
                    int Search_Menu_Index = Search_Menu.Run();
                    Clear();
                    while (true)

                    {
                        if (Search_Menu_Index == -1)
                            break;
                        //Title & Author.
                        if (Search_Menu_Index == 0)
                        {
                            string Title, Author;
                            List<Book> temp_List;
                            ConsoleKeyInfo User_Choois;
                            try
                            {
                                (User_Choois, Title) = Menu.CaptureExitKey("ESC\n-----Search-----\nEnter the Title: ");
                                if (User_Choois.Key == ConsoleKey.Escape)
                                    break;

                                Clear();
                                (User_Choois, Author) = Menu.CaptureExitKey($"ESC\n-----Search-----\nEnter the Title: {Title}\nEnter the Authro: ");
                                if (User_Choois.Key == ConsoleKey.Escape)
                                    break;

                                temp_List = library.Search(Title, Author);
                                if (temp_List.Count == 0)
                                {
                                    Clear();
                                    WriteLine("The book not found.");
                                }
                                else
                                {
                                    Clear(); WriteLine("Matching results:");
                                    library.Group_Dispaly(temp_List);
                                    int Array_Borrow_Size = Library.Available_Book(temp_List);
                                    if (Array_Borrow_Size > 0)
                                    {
                                        WriteLine("Do you want to borrow one of the available books? (Y/N)");
                                        if (Menu.Answer_Y_N().Key == ConsoleKey.Y)
                                        {
                                            Menu temp_Menu = Menu.Creat_Borrow_Menu(temp_List, Array_Borrow_Size);
                                            int index_Borrow_Book = temp_Menu.Run();
                                            string ttt = temp_Menu.Option[index_Borrow_Book];
                                            foreach (Book book in temp_List)
                                            {
                                                if ($"{book.Title} by {book.Author}. /({book.Subject})." == ttt)
                                                    book.IsAvailable = false;
                                            }
                                            WriteLine("The book will reach you soon :)");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Clear();
                                WriteLine(ex.Message);
                                WriteLine("Enter the Title again\n");
                                continue;
                            }
                            WriteLine("Another Search? (Y/N)");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //Title OR Author.
                        else if (Search_Menu_Index == 1)
                        {
                            List<Book> temp_List_Author;
                            List<Book> temp_List_Title;
                            ConsoleKeyInfo User_Choois;
                            string TitleOrAuthor;

                            (User_Choois, TitleOrAuthor) = Menu.CaptureExitKey("ESC.\n-----Search-----\nEnter the Title OR Author:");
                            if (User_Choois.Key == ConsoleKey.Escape)
                                break;

                            (temp_List_Author, temp_List_Title) = library.SearchByBoth(TitleOrAuthor);

                            if (temp_List_Author.Count != 0)
                            {
                                WriteLine("Books that match the Author:");
                                library.Group_Dispaly(temp_List_Author);
                            }
                            else
                                WriteLine("There is no Author Match");

                            WriteLine();
                            if (temp_List_Title.Count != 0)
                            {
                                WriteLine("Books that match the Title");
                                library.Group_Dispaly(temp_List_Title);
                            }
                            else
                            {
                                WriteLine("there is no book match the title");
                                if (temp_List_Author.Count == 0 && temp_List_Title.Count == 0)
                                {
                                    WriteLine("Another search? Y/N");
                                    if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                        break;
                                    else
                                        continue;
                                }
                            }
                            WriteLine("Do you want to borrow one of the available books? (Y/N)");

                            if (Menu.Answer_Y_N().Key == ConsoleKey.Y)
                            {
                                List<Book> temp_List = MyMothod<Book>.merge(temp_List_Author, temp_List_Title);
                                int Size_Array = Library.Available_Book(temp_List);
                                Menu temp_Borrow_Menu = Menu.Creat_Borrow_Menu(temp_List, Size_Array);
                                int index_Borrow_Book = temp_Borrow_Menu.Run();
                                string ttt = temp_Borrow_Menu.Option[index_Borrow_Book];

                                foreach (Book book in temp_List)
                                {
                                    if ($"{book.Title} by {book.Author}. /({book.Subject})." == ttt)
                                        book.IsAvailable = false;
                                }
                                WriteLine("The book will reach you soon :)");
                            }
                            WriteLine("Another search? (Y/N)");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //Subject.
                        else if (Search_Menu_Index == 2)
                        {
                            //this line will return the index if Selected Subject
                            //and it is treated as an Arguments to the(Books_Subject)
                            //which will return a list of books related to the Selected Subject.
                            int index = Subject_Menu.Run();
                            //if Back
                            if (index == -1)
                                break;

                            library.Books_Subject(Subject_Menu_Option[index]);
                            WriteLine("Another Search? (Y/N)");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //random books .
                        else if (Search_Menu_Index == 3)
                        {
                            library.DisPlayRandomBook();
                            WriteLine("Another Search? Y/N");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //Back
                        else if (Search_Menu_Index == 4)
                            break;
                    }
                }
                //Add
                if (Start_Menu_Index == 1)
                {
                    ConsoleKeyInfo User_Choois;
                    string Title;
                    string Author;
                    Clear();
                    while (true)
                    {
                        try
                        {
                            (User_Choois, Title) = Menu.CaptureExitKey("ESC.\n---Add a book---\nEnter the title: ");

                            if (User_Choois.Key == ConsoleKey.Escape)
                                break;
                            Clear();
                            (User_Choois, Author) = Menu.CaptureExitKey($"ESC.\n---Add a book---\nEnter the title: {Title} \nEnter the author: ");
                            if (User_Choois.Key == ConsoleKey.Escape)
                                break;

                            Clear();
                            //in this line it was possible to create a variable to hold
                            //the string value from (Subject_Menu_Option[Subject_Menu.Run()]))
                            //and then but this variable as an Arguments ( The subject of the book)
                            //but I did not do that because it is a redundant variable.
                            int index = Subject_Menu.Run();
                            if (index == 5)
                                break;

                            library.Add(new Book(Title, Author, Subject_Menu_Option[index]));
                        }
                        catch (Exception ex)
                        {
                            Clear();
                            WriteLine(ex.Message);
                            continue;
                        }
                        WriteLine("add another book?\n Y/N");
                        if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                            break;
                        else
                            continue;
                    }
                }
                // Exit
                if (Start_Menu_Index == 2)
                {
                    Check = false;
                    Clear();
                    WriteLine("Good_Bay!");
                }
            }
        }
    }
}