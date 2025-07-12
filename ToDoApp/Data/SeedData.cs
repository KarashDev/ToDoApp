using ToDoApp.Models;
using ToDoApp.Data;

namespace ToDoApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (context.TodoItems.Any()) return;

            var initial = new[]
            {
            new TodoItem { Title = "Купить хлеб" },
            new TodoItem { Title = "Позвонить маме", IsCompleted = true },
            new TodoItem { Title = "Выучить C#", IsCompleted = false }
        };

            context.TodoItems.AddRange(initial);
            await context.SaveChangesAsync();
        }
    }
}
