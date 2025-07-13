using ToDoApp.DTOs;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface ITodoService
    {
        Task<List<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto?> GetByIdAsync(Guid id);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto createDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
