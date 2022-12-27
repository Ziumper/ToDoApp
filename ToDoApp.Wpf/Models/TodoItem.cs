namespace ToDoApp.Wpf.Models
{
    public class TodoItem
    {
        public string Description { get; set; }
        public bool IsChecked { get; set; }

        public TodoItem()
        {
            Description = string.Empty;
        }
    }
}