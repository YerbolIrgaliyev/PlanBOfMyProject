using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public static class UsersExporter
    {
        public static void WriteToFile(string path, List<User> users)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                string jsonStr = JsonConvert.SerializeObject(users);
                file.WriteLine(jsonStr);
            }
        }
    }
}
