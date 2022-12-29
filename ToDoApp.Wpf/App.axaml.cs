using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
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
                Database database = new ();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(database),
                };

                desktop.Exit += OnExit;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            if(sender is not IClassicDesktopStyleApplicationLifetime desktop) return;
            if (desktop.MainWindow.DataContext is not MainWindowViewModel viewModel) return;

            Database database = new ();
            database.Save(viewModel.List.Items);
        }
    }
}
