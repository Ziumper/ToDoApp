using ReactiveUI;
using System.Reactive;
using ToDoApp.Wpf.Models;

namespace ToDoApp.Wpf.ViewModels
{
    public class AddItemViewModel : ViewModelBase
    {
        private string description;

        public AddItemViewModel()
        {
            description = string.Empty;
            var okEnabled = this.WhenAnyValue(
               x => x.Description,
               x => !string.IsNullOrWhiteSpace(x));

            Ok = ReactiveCommand.Create(
                () => new TodoItem { Description = Description },
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }

        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}