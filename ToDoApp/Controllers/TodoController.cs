using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IValidator<CreateTodoItemDto> _validator;

        public TodoController(ITodoService todoService, IValidator<CreateTodoItemDto> validator)
        {
            _todoService = todoService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItemDto>> GetAll() => await _todoService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetById(Guid id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto createDto)
        {
            var validationResult = await _validator.ValidateAsync(createDto);
            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors);

            var created = await _todoService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _todoService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
