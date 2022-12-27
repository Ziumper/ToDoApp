using System;
using System.Collections.Generic;
using ToDoApp.Wpf.Models;

namespace ToDoApp.Wpf.Services
{
    public class Database
    {
        public IEnumerable<TodoItem> GetItems() => Array.Empty<TodoItem>();
    }
}