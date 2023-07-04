﻿using HR_Management.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationDetailRequest : IRequest<LeaveAllocationDto>
{
    public long Id { get; set; }
}