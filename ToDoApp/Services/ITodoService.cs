using ToDoApp.DTOs;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<TodoItem> CreateAsync(TodoItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
