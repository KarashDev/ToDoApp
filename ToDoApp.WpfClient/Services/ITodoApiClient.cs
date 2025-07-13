using ToDoApp.WpfClient.Models;

namespace ToDoApp.WpfClient.Services
{
    public interface ITodoApiClient
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<bool> CreateAsync(string title);
        Task<bool> DeleteAsync(Guid id);
    }
}
