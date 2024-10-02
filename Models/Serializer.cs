using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serialization.Models
{
    internal class Serializer
    {
        public static void Serialize(string path)
        {
            string jsonString = JsonSerializer.Serialize(Program.DataBase);
            File.WriteAllText(path, jsonString);
        }

        public static Account[] Deserialization(string path)
        {
            string content = File.ReadAllText(path);
            Account[] accounts = JsonSerializer.Deserialize<Account[]>(content);
            return accounts;
        }
    }
}
