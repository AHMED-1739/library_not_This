using System;
using System.Collections.Generic;
using static System.Console;

namespace dosomething
{
    class Menu
    {
        public readonly string[] Option;
        readonly string Title;
        public int Selected_Index;
        public Menu(string[] Option, string Title)
        {
            this.Option = Option;
            this.Title = Title;
            Selected_Index = 0;
        }
        //only used in run method.
        void View()
        {
            if (Selected_Index >= Option.Length)
                Selected_Index = 0;
            WriteLine(Title + "\n");
            for (int i = 0; i < Option.Length; i++)
            {
                if (i != Selected_Index)
                    WriteLine($"-{Option[i]}");
                else
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    WriteLine($"-{Option[i]}  <<");
                    ResetColor();
                }
            }
        }
        // this method will run the Menu.
        public int Run()
        {
            ConsoleKeyInfo KeyPressed;
            do
            {
                Clear();
                View();
                KeyPressed = ReadKey(true);
                if (KeyPressed.Key == ConsoleKey.UpArrow)
                {
                    Selected_Index--;
                    if (Selected_Index == -1)
                        Selected_Index = Option.Length - 1;
                }
                else if (KeyPressed.Key == ConsoleKey.DownArrow)
                {
                    Selected_Index++;
                    if (Selected_Index > Option.Length)
                        Selected_Index = 0;
                }
                else if (KeyPressed.Key == ConsoleKey.Escape)
                    return -1;
            } while (KeyPressed.Key != ConsoleKey.Enter);
            Clear();
            return Selected_Index;
        }
        // just to ask the user if he want to repeate the process or not
        static public ConsoleKeyInfo Answer_Y_N()
        {
            ConsoleKeyInfo Choois;

            while (true)
            {
                Choois = ReadKey(true);
                if (Choois.Key == ConsoleKey.Y || Choois.Key == ConsoleKey.N)
                {
                    Clear();
                    break;
                }
                if (Choois.Key != ConsoleKey.Y || Choois.Key != ConsoleKey.N)
                    continue;
            }
            return Choois;
        }
        static public (ConsoleKeyInfo, string) CaptureExitKey(string Title)
        {
            ConsoleKeyInfo KeyPressed;
            string TheString = "";
            Write(Title);
            do
            {
                KeyPressed = ReadKey(true);
                if (KeyPressed.Key == ConsoleKey.Backspace)
                {
                    if (TheString.Length > 0)
                    {
                        Clear();
                        Write(Title);
                        TheString = TheString.Remove(TheString.Length - 1);
                        Write(TheString);
                    }
                    continue;
                }
                if (KeyPressed.KeyChar != 13 && KeyPressed.KeyChar != 27)
                {
                    Write(KeyPressed.KeyChar);
                    TheString += KeyPressed.KeyChar;
                }
            } while (KeyPressed.Key != ConsoleKey.Escape && KeyPressed.Key != ConsoleKey.Enter);
            WriteLine();
            return (KeyPressed, TheString);
        }
        static public Menu Creat_Borrow_Menu(List<Book> books, int length1, List<Book> books2 = null)
        {
            string[] temp_array = new string[length1];
            int index = -1;

            foreach (Book book in books)
            {
                if (book.IsAvailable)
                {
                    temp_array[++index] = $"{book.Title} by {book.Author}. /({book.Subject}).";
                }
            }
            return new Menu(temp_array, "----Borrow a book----"); ;
        }
    }
}