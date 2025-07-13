using ToDoApp.WpfClient.Models;

namespace ToDoApp.WpfClient.Services
{
    public interface ITodoApiClient
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<TodoItem?> CreateAsync(string title);
        Task<bool> DeleteAsync(Guid id);
    }
}
