namespace ToDoApp.Validators
{
    using FluentValidation;
    using ToDoApp.DTOs;

    public class TodoItemDtoValidator : AbstractValidator<TodoItemDto>
    {
        public TodoItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Заголовок не может быть пустым")
                .MaximumLength(100).WithMessage("Длина заголовка не должна превышать 100 символов");
        }
    }
}
