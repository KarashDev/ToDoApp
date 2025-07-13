using FluentValidation;
using ToDoApp.DTOs;

namespace ToDoApp.Validators
{
    public class TodoItemDtoValidator : AbstractValidator<CreateTodoItemDto>
    {
        public TodoItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Заголовок не может быть пустым")
                .MaximumLength(100).WithMessage("Длина заголовка не должна превышать 100 символов");
        }
    }
}
