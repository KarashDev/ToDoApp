using ToDoApp.DTOs;
using ToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using AutoMapper;

namespace ToDoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public TodoService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<TodoItemDto>> GetAllAsync()
        {
            var items = await _db.TodoItems.ToListAsync();
            return _mapper.Map<List<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto?> GetByIdAsync(Guid id)
        {
            var item = await _db.TodoItems.FindAsync(id);
            return item == null ? null : _mapper.Map<TodoItemDto>(item);
        }

        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto)
        {
            var entity = _mapper.Map<TodoItem>(dto);
            entity.Id = Guid.NewGuid();
            entity.IsCompleted = false;

            _db.TodoItems.Add(entity);
            await _db.SaveChangesAsync();

            return new TodoItemDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                IsCompleted = entity.IsCompleted
            };
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
