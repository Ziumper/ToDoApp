using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.Wpf.Models;

namespace ToDoApp.Wpf.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        public TodoListViewModel(IEnumerable<TodoItem> items)
        {
            Items = new ObservableCollection<TodoItem>(items);
        }

        public ObservableCollection<TodoItem> Items { get; }
    }
}