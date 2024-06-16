
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Connect_DB
{
    internal class Program
    {
        static void colorRedW(string S)//покрас сторки в цвет:
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(S);
            Console.ForegroundColor = color;
        }
        static void colorWL(string S)//покрас сторки в цвет:
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(S);
            Console.ForegroundColor = color;
        }

        static void Main()
        {
            DBHelper db = new DBHelper();
            db.LoadDB();
            //db.Create_DB();
            db.Connect_DB();
            //db.AddToDB();
            //db.ReadAllDB();
            int command = 0;
            while (true)
            {
                //Console.WriteLine("введите команду: add, read, del, delN, up");
                Console.WriteLine("Выберите действие");
                if (command == 0) {  colorWL("* добавить"); }
                else Console.WriteLine("* добавить");
                if (command == 1) {colorWL("* читать"); }
                else Console.WriteLine("* читать");
                if (command == 2) { colorWL("* удалить"); }
                else Console.WriteLine("* удалить");
                if (command == 3) { colorWL("* удалить по имени"); }
                else Console.WriteLine("* удалить по имнени");
                if (command == 4) { colorWL("* обновить"); }
                else Console.WriteLine("* обновить");
                Console.WriteLine("для выбора нажмите Enter");
                ConsoleKeyInfo charKey = Console.ReadKey();//нажимаем определённую клавишу
                switch (charKey.Key)
                {
                    case ConsoleKey.UpArrow: if (command > 0) command--; break;
                    case ConsoleKey.DownArrow: if (command < 4) command++; break;
                    case ConsoleKey.Enter:
                        switch (command)
                        {
                            case 0: db.AddToDB(); break;
                            case 1: db.ReadAllDB(); break;
                            case 2: db.DeleteToDB(); break;
                            case 3: db.DeleteForNameDB(); break;
                            case 4: db.UpdateToDB(); break;
                            default: Console.WriteLine("неверная команда"); break;
                        }
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            }
        }
    }
    
}
