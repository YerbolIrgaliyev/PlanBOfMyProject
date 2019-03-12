using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Control
{
    public static class UsersImporter
    {
        public static List<User> ReadFile(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string jsonStr = File.ReadAllText(path);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonStr);
                return users;
            }
        }
    }
}
