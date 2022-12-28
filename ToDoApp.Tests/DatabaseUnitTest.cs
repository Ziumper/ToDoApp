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
            Database database = new ();
            IEnumerable<TodoItem> items = database.GetItems();

            List<TodoItem> todoItems = items.ToList();
            TodoItem item = new()
            {
                Description = "description",
                IsChecked = true,
            };
           
            todoItems.Add(item);
            database.Save(todoItems);
            IEnumerable<TodoItem> loadedItems = database.Load();

            Assert.Equal(loadedItems.Count(),todoItems.Count());
        }
    }
}
