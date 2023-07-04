using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler:IRequestHandler<CreateLeaveAllocationCommand,long>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<long> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        #region validation

        var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        #endregion
        var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
        leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
        return leaveAllocation.Id;
    }
}