using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToDoApp.Wpf.Models;

namespace ToDoApp.Wpf.Services
{
    public class Database
    {
        private readonly string dataPath;
        private readonly string directoriesPath;

        public Database()
        {
            directoriesPath = DirectoriesPath;
            dataPath = DataPath;
        }

        public void Save(IEnumerable<TodoItem> todoItems)
        {
            string json = JsonSerializer.Serialize(todoItems);

            if (Directory.Exists(dataPath) == false)
            {
                string directory = directoriesPath;
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(dataPath, json);
        }

        public IEnumerable<TodoItem> Load()
        {
            if (File.Exists(dataPath) == false)
            {
                return Array.Empty<TodoItem>();
            }

            string json = File.ReadAllText(dataPath);
            if (string.IsNullOrEmpty(json) || json == null)
            {
                return Array.Empty<TodoItem>();
            }

            IEnumerable<TodoItem>? result = JsonSerializer.Deserialize<IEnumerable<TodoItem>>(json);

            if (result == null)
            {
                return Array.Empty<TodoItem>();
            }

            return result;
        }

        public void DeleteAll()
        {
            if (File.Exists(dataPath)) File.Delete(dataPath);
        }

        public void Delete(IEnumerable<TodoItem> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TodoItem> GetItems() => Load();

        public static string FileName => "ToDoList.json";

        public string DataPath
        {
            get
            {
                if (string.IsNullOrEmpty(dataPath))
                {
                    string path = directoriesPath;
                    path += FileName;
                    return path;
                }

                return dataPath;
            }
        }

        public string DirectoriesPath
        {
            get
            {
                if (string.IsNullOrEmpty(directoriesPath))
                {
                    string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                      + Path.DirectorySeparatorChar + "Ziumper" + Path.DirectorySeparatorChar + "ToDoApp" + Path.DirectorySeparatorChar;
                    return directory;
                }

                return directoriesPath;
            }
        }
    }
}