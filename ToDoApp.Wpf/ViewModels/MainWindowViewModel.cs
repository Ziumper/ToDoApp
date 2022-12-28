using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using ToDoApp.Wpf.Models;
using ToDoApp.Wpf.Services;

namespace ToDoApp.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? content;
        private Database database;

        public MainWindowViewModel(Database database)
        {
            this.database = database;
            Content = List = new TodoListViewModel(database.GetItems());
        }

        public ViewModelBase? Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; private set; }

        public void AddItem()
        {
            AddItemViewModel vm = new ();

            _ = Observable.Merge(
              vm.Ok,
              vm.Cancel.Select(_ => null as TodoItem))
              .Take(1)
              .Subscribe(model =>
              {
                  if (model != null)
                  {
                      List.Items.Add(model);
                  }

                  Content = List;
              });

            Content = vm;
        }

        public void DeleteAll()
        {
            database.DeleteAll();
            Content = List = new TodoListViewModel(database.GetItems());
        }
    }
}