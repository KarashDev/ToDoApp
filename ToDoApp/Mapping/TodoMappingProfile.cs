using AutoMapper;
using ToDoApp.DTOs;
using ToDoApp.Models;

namespace ToDoApp.Mapping
{
    public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
            CreateMap<CreateTodoItemDto, TodoItem>();
            CreateMap<CreateTodoItemDto, TodoItemDto>();
        }
    }
}
