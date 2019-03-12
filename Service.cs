using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Console;

namespace Control
{
    public class Service
    {
        const int MAX_OPTIONS = 3;
        const string PATH_TO_FILE = @"D:\3\users.json";
        const string PATH_TO_DIRECTORY = @"D:\3\";

        private List<User> users;

        public void Run()
        {
            string choiceStr;
            int choiceInt;

            if (!Directory.Exists(PATH_TO_DIRECTORY))
            {
                Directory.CreateDirectory(PATH_TO_DIRECTORY);
            }

            if (File.Exists(PATH_TO_FILE))
            {
                users = UsersImporter.ReadFile(PATH_TO_FILE);
            }
            else
            {
                users = new List<User>();
            }

            while (true)
            {
                WriteLine("Введите действие:\n1) Регистрация\n2) Вход\n3) Выход из приложения");
                choiceStr = ReadLine();
                if (int.TryParse(choiceStr, out choiceInt))
                {
                    if (choiceInt > 0 && choiceInt <= MAX_OPTIONS)
                    {
                        switch (choiceInt)
                        {
                            case 1:
                                {
                                    if (Registration.AddUser(users))
                                    {
                                        UsersExporter.WriteToFile(PATH_TO_FILE, users);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    string loginStr, psswdStr = "";
                                    WriteLine("Введите логин:");
                                    loginStr = ReadLine();
                                    if (!string.IsNullOrWhiteSpace(loginStr))
                                    {
                                        WriteLine("Введите пароль:");
                                        do
                                        {
                                            ConsoleKeyInfo key = Console.ReadKey(true);
                                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                                            {
                                                psswdStr += key.KeyChar;
                                                Console.Write("*");
                                            }
                                            else
                                            {
                                                if (key.Key == ConsoleKey.Backspace && psswdStr.Length > 0)
                                                {
                                                    psswdStr = psswdStr.Substring(0, (psswdStr.Length - 1));
                                                    Console.Write("\b \b");
                                                }
                                                else if (key.Key == ConsoleKey.Enter)
                                                {
                                                    Console.WriteLine();
                                                    break;
                                                }
                                            }
                                        } while (true);
                                        if(Login.Access(users, loginStr, psswdStr))
                                        {
                                            for(int i = 0; i < users.Count; i++)
                                            {
                                                if(users[i].Login == loginStr)
                                                {
                                                    if (!string.IsNullOrWhiteSpace(users[i].FullName))
                                                    {
                                                        WriteLine($"Добро пожаловать, {users[i].FullName}!");
                                                    }
                                                    else WriteLine($"Добро пожаловать, {loginStr}!");
                                                   
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                        }
                    }
                }
                
            }
            Environment.Exit(0); 
        }
    }
}
