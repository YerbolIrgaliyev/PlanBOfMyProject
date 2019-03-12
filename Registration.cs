using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using static System.Console;

namespace Control
{
    public static class Registration
    {
        const int MIN_AGE = 0;
        const int MIN_PSSWD_LEN = 6;
        public static bool AddUser(List<User> users)
        {
            User newUser = new User();
            string usLoginStr, usPsswdStr, usFullNameStr,
                    usEmailStr, usPhoneStr, usAgeStr;

            WriteLine("Введите логин");
            usLoginStr = ReadLine();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == usLoginStr)
                {
                    WriteLine("Логин уже занят!");
                    WriteLine("Регистрация прервана.");
                    return false;
                }
            }
            if (CheckUsername(usLoginStr))
            {
                WriteLine("Введите пароль(больше 6 символов):");
                usPsswdStr = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        usPsswdStr += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && usPsswdStr.Length > 0)
                        {
                            usPsswdStr = usPsswdStr.Substring(0, (usPsswdStr.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine();
                            break;
                        }
                    }
                } while (true);
                if (CheckPassword(usPsswdStr))
                {
                    WriteLine("Введите полное имя:");
                    usFullNameStr = ReadLine();
                    WriteLine("Введите email");
                    usEmailStr = ReadLine();

                    if (CheckEmail(usEmailStr))
                    {
                        WriteLine("Введите номер телефона:");
                        usPhoneStr = ReadLine();

                        if (CheckPhoneNumber(usPhoneStr))
                        {
                            WriteLine("Введите возраст:");
                            usAgeStr = ReadLine();
                            int ageInt;

                            if (int.TryParse(usAgeStr, out ageInt))
                            {
                                if (ageInt > MIN_AGE)
                                {
                                    newUser.Login = usLoginStr.Trim();
                                    newUser.Password = usPsswdStr;
                                    newUser.FullName = usFullNameStr.Trim();
                                    newUser.Email = usEmailStr.Trim();
                                    newUser.PhoneNumber = usPhoneStr.Trim();
                                    newUser.Age = ageInt;

                                    users.Add(newUser);
                                    return true;
                                }
                                else WriteLine($"Возраст должен быть больше {MIN_AGE}!");
                            }
                            else WriteLine("Да вы издеваетесь!");
                        }
                        else WriteLine("Неверный формат телефона!");
                    }
                    else WriteLine("Неверный формат email!");
                }
                else WriteLine("Пароль недостаточно длинный!");
            }

            WriteLine("Регистрация прервана.");
            return false;
        }
        private static bool CheckUsername(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                return true;
            }
            return false;
        }
        private static bool CheckPassword(string userPsswd)
        {
            if (!string.IsNullOrWhiteSpace(userPsswd))
            {
                if(userPsswd.Length > MIN_PSSWD_LEN)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool CheckFullName(string userFullName)
        {
            if (!string.IsNullOrWhiteSpace(userFullName))
            {
                return true;
            }
            return false;
        }
        private static bool CheckEmail(string userEmail)
        {
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(userEmail);
                    return addr.Address == userEmail;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        private static bool CheckPhoneNumber(string userPhone)
        {
            var phoneNumber = userPhone.Trim()
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("(", "")
            .Replace(")", "");
            return Regex.Match(phoneNumber, @"^\+\d{5,15}$").Success;
        }
    }
}
