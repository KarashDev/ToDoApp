using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoApp.Data;
using ToDoApp.Services;
using ToDoApp.Validators;
using ToDoApp.DTOs;

// 🛠️ Инициализация SQLite провайдера
SQLitePCL.Batteries_V2.Init(); // <-- Важно!

var builder = WebApplication.CreateBuilder(args);

// Добавление контроллеров и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Entity Framework Core — SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

// DI для сервиса
builder.Services.AddScoped<ITodoService, TodoService>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<TodoItemDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Инициализация базы и сидов
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.InitializeAsync(db);
}

app.Run();
