using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class ILeaveRequestDtoValidator:AbstractValidator<ILeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;


        RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate)
            .WithMessage("{PropertyName} باید قبل از {ComparisonValue} باشد");

        RuleFor(p => p.EndDate)
            .GreaterThan(p => p.StartDate)
            .WithMessage("{PropertyName} باید بزرگتر از {ComparisonValue} باشد");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(async (id, token) =>
            {
                var leaveTypeExist = await _leaveTypeRepository.Exist(id);
                return !leaveTypeExist;
            })
            .WithMessage("{PropertyName} وجود ندارد");
    }


}