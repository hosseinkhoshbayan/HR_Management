using HR_Management.Application.DTOs.LeaveType;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDto>
{
    public long Id { get; set; }
}