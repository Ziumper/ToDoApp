using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ToDoApp.Wpf.Services;
using ToDoApp.Wpf.ViewModels;
using ToDoApp.Wpf.Views;

namespace ToDoApp.Wpf
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Database database = new Database();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(database),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
