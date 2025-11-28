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

        public static List<T> LoadAll<T>(string folder_path)
        {
            if (!Directory.Exists(folder_path))
            {
                string alt_path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    folder_path.Replace(Directory.GetCurrentDirectory(), "").TrimStart('\\')
                );

                if (Directory.Exists(alt_path))
                    folder_path = alt_path;
                else
                    throw new Exception("Dossier introuvable : " + folder_path);
            }

            List<T> list = new List<T>();
            string[] files = Directory.GetFiles(folder_path, "*.json");

            for (int i = 0; i < files.Length; i++)
            {
                string json = File.ReadAllText(files[i]);
                T obj = JsonSerializer.Deserialize<T>(json, _options);

                if (obj != null)
                {
                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
