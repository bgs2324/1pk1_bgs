using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practicbgs
{

    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    public static class DataManager
    {
        private static string jsonFilePath = "clients.json";

        public static void SaveData(Dictionary<string, Client> clients)
        {
            string jsonString = JsonSerializer.Serialize(clients);
            File.WriteAllText(jsonFilePath, jsonString);
        }

        public static Dictionary<string, Client> LoadData()
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonString = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<Dictionary<string, Client>>(jsonString);
            }
            return null;
        }
    }
}
