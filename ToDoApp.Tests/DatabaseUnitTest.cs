using ToDoApp.Wpf.Models;
using ToDoApp.Wpf.Services;

namespace ToDoApp.Tests
{
    public class DatabaseUnitTest
    {
        [Fact]
        public void IsZeroOnStart()
        {
            Database database = new();

            IEnumerable<TodoItem> items = database.GetItems();

            int count = items.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public void IsDatabasePathNotEmpty()
        {
            Database database = new();
            Assert.False(string.IsNullOrEmpty(database.DataPath));
        }

        [Fact]
        public void IsSavedTodoItemsEqualToLoaded()
        {
            IEnumerable<TodoItem> items = SaveTodoItems();

            Database database = new ();
            IEnumerable<TodoItem> loadedItems = database.Load();

            Assert.Equal(loadedItems.Count(), items.Count());
            database.DeleteAll();
        }

        [Fact]
        public void DeletedAllTodoItemsAfterSaving()
        {
            Database database = new();
            SaveTodoItems();
            database.DeleteAll();

            IEnumerable<TodoItem> loadedItems = database.Load();
            Assert.Empty(loadedItems);
        }

        [Fact]
        public void CanCreateDirectoryWhenSave()
        {
            Database database = new();

            if (Directory.Exists(database.DirectoriesPath))
            {
                DeleteFiles();      
            }

            SaveTodoItems();
            
            Assert.True(Directory.Exists(database.DirectoriesPath));
            database.DeleteAll();
        }

        [Fact]
        public void IsTheNameOfSavedFileIsCorrectInPath()
        {
            Database database = new();
            string fileName = database.DataPath.Split(Path.DirectorySeparatorChar).Last();
            Assert.Equal(Database.FileName, fileName);
        }


        private static IEnumerable<TodoItem> SaveTodoItems()
        {
            Database database = new();
            
            List<TodoItem> todoItems = new ();
            TodoItem item = new()
            {
                Description = "description",
                IsChecked = true,
            };

            todoItems.Add(item);
            database.Save(todoItems);

            return todoItems;
        }

        private static void DeleteFiles()
        {
            Database database = new ();
            database.DeleteAll();
            Directory.Delete(database.DirectoriesPath);
        }

    }
}