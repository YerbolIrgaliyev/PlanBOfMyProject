using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public static class Login
    {
        public static bool Access(List<User> users, string userLogin, string userPsswd)
        {
            for(int i = 0; i < users.Count; i++)
            {
                if(userLogin.Trim() == users[i].Login)
                {
                    if(userPsswd == users[i].Password)
                    {
                        return true;
                    }
                }
            }
            Console.WriteLine("Неверный логин или пароль пользователя!");
            return false;
        }
    }
}
