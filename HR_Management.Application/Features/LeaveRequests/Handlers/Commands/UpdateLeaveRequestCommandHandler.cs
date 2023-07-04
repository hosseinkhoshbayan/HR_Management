using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Contracts.Persistence;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        #region validation

        var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);

        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        #endregion

        var leaveRequest = await _leaveRequestRepository.Get(request.Id);
        if (request.UpdateLeaveRequestDto != null)
        {
            _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
            await _leaveRequestRepository.Update(leaveRequest);
        }
        else if (request.ChangeLeaveRequestApprovalDto != null)
        {
            await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
        }

        return Unit.Value;
    }
}