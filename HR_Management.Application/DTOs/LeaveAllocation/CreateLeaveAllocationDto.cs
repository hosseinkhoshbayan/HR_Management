﻿using HR_Management.Application.DTOs.Common;
using HR_Management.Application.DTOs.LeaveType;

namespace HR_Management.Application.DTOs.LeaveAllocation;

public class CreateLeaveAllocationDto : BaseDto, ILeaveAllocationDto
{
    public int NumberOfDays { get; set; }

    public long LeaveTypeId { get; set; }

    public int Priod { get; set; }
}