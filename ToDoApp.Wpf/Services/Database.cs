using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using ToDoApp.Wpf.Models;

namespace ToDoApp.Wpf.Services
{
    public class Database
    {
        private readonly string dataPath;

        public Database()
        {
            dataPath = DataPath;
        }

        public void Save(IEnumerable<TodoItem> todoItems)
        {
            string json = JsonSerializer.Serialize(todoItems);
            File.WriteAllText(dataPath, json);
        }

        public IEnumerable<TodoItem> Load()
        {
            if (File.Exists(dataPath) == false)  {
                return Array.Empty<TodoItem>();
            } 
            
            string json = File.ReadAllText(dataPath);
            if(string.IsNullOrEmpty(json) || json == null)
            {
                return Array.Empty<TodoItem>();
            }

            IEnumerable<TodoItem>? result = JsonSerializer.Deserialize<IEnumerable<TodoItem>>(json);
            
            if(result == null)
            {
                return Array.Empty<TodoItem>();
            }

            return result;
        }

        public void DeleteAll()
        {
            if(File.Exists(dataPath)) File.Delete(dataPath);
        }

        public IEnumerable<TodoItem> GetItems() => Load();
        
        public string DataPath
        {
            get
            {
                if (string.IsNullOrEmpty(dataPath))
                {
                    string assemblyPath = AppContext.BaseDirectory;
                    string path = assemblyPath;
                    path += "ToDoList.json";
                    return path;
                }

                return dataPath;
            }
        }

    }
}