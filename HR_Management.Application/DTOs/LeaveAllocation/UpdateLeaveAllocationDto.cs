﻿using HR_Management.Application.DTOs.Common;

namespace HR_Management.Application.DTOs.LeaveAllocation;

public class UpdateLeaveAllocationDto:BaseDto, ILeaveAllocationDto
{
    public int NumberOfDays { get; set; }

    public long LeaveTypeId { get; set; }

    public int Priod { get; set; }
}