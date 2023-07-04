using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Commands;

public class DeleteLeaveAllocationCommand:IRequest<Unit>
{
    public long Id { get; set; }
}