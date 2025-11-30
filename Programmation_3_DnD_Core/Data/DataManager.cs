using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Programation_3_DnD_Core
{
    public static class DataManager
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Include
        };

        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Fichier introuvable : " + path);
            }

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public static List<T> LoadAll<T>(string folder_path)
        {
            if (!Path.IsPathRooted(folder_path))
                throw new Exception("Le chemin fourni doit être absolu : " + folder_path);

            if (!Directory.Exists(folder_path))
                throw new Exception("Dossier introuvable : " + folder_path);

            List<T> list = new List<T>();
            string[] files = Directory.GetFiles(folder_path, "*.json");

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                T objet = JsonConvert.DeserializeObject<T>(json, _settings);

                if (objet != null)
                    list.Add(objet);
            }

            return list;
        }
    }
}
