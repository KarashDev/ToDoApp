using System.Net.Http;
using System.Net.Http.Json;
using ToDoApp.WpfClient.Models;

namespace ToDoApp.WpfClient.Services
{
    public class TodoApiClient : ITodoApiClient
    {
        private readonly HttpClient _httpClient;

        public TodoApiClient(string baseUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/todo") ?? new List<TodoItem>();
        }

        public async Task<bool> CreateAsync(string title)
        {
            var dto = new { Title = title };
            var response = await _httpClient.PostAsJsonAsync("api/todo", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/todo/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
