using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveType.Validators;

public class ILeaveTypeDtoValidator:AbstractValidator<ILeaveTypeDto>
{
    public ILeaveTypeDtoValidator()
    {
        RuleFor(p=>p.Name).NotEmpty().WithMessage("{PropertyName} الزامی است.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName}  نمی تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(p => p.DefaultDay)
            .NotEmpty().WithMessage("{PropertyName} الزامی است.")
            .GreaterThan(0).WithMessage("{PropertyName}  باید بیشتر از 0 باشد")
            .LessThan(100).WithMessage("{PropertyName}  باید کمتر از 100 باشد");
    }
}