using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler:IRequestHandler<CreateLeaveTypeCommand,long>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<long> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        #region validation

        var validator = new CreateLeaveTypeValidator();
        var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDto);

        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        #endregion


        var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
        leaveType = await _leaveTypeRepository.Add(leaveType);
        return leaveType.Id;
    }
}