using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Programation_3_DnD.Data
{
    public static class DataManager
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };

        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Fichier introuvable : " + path);
            }

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public static List<T> LoadAll<T>(string folderPath)
        {
            if (!Path.IsPathRooted(folderPath))
                throw new Exception("Le chemin fourni doit être absolu : " + folderPath);

            if (!Directory.Exists(folderPath))
                throw new Exception("Dossier introuvable : " + folderPath);

            List<T> list = new List<T>();
            string[] files = Directory.GetFiles(folderPath, "*.json");

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                T obj = JsonSerializer.Deserialize<T>(json, _options);

                if (obj != null)
                    list.Add(obj);
            }

            return list;
        }
    }
}
