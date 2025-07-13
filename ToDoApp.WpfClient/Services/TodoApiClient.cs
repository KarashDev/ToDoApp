using AutoMapper;
using System.Net.Http;
using System.Net.Http.Json;
using ToDoApp.WpfClient.DTOs;
using ToDoApp.WpfClient.Models;

namespace ToDoApp.WpfClient.Services
{
    public class TodoApiClient : ITodoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public TodoApiClient(string baseUrl, IMapper mapper)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _mapper = mapper;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<TodoItemDto>>("api/todo");
            return dtos.Select(d => new TodoItem() { Id = d.Id, Title = d.Title, IsCompleted = d.IsCompleted }).ToList();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            var dto = await _httpClient.GetFromJsonAsync<TodoItemDto>($"api/todo/{id}");
            return dto == null ? null : _mapper.Map<TodoItem>(dto);
        }

        public async Task<TodoItem> CreateAsync(string title)
        {
            var createDto = new CreateTodoItemDto { Title = title };
            var response = await _httpClient.PostAsJsonAsync("api/todo", createDto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TodoItemDto>();
            return new TodoItem() { Id = result.Id, Title = result.Title, IsCompleted = result.IsCompleted };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/todo/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
