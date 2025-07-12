using ToDoApp.DTOs;
using ToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;

namespace ToDoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _db;

        public TodoService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _db.TodoItems.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            return await _db.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItemDto dto)
        {
            var item = new TodoItem { Title = dto.Title };
            _db.TodoItems.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _db.TodoItems.FindAsync(id);
            if (item == null) return false;

            _db.TodoItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
