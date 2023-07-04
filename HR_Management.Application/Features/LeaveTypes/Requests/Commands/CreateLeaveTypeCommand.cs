using HR_Management.Application.DTOs.LeaveType;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand:IRequest<long>
{
    public CreateLeaveTypeDto CreateLeaveTypeDto { get; set; }
}