namespace ToDoApp.WpfClient.DTOs
{
    public class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
