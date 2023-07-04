using HR_Management.Application.DTOs.Common;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Domain;

namespace HR_Management.Application.DTOs.LeaveAllocation;

public class LeaveAllocationDto : BaseDto
{
    public int NumberOfDays { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public long LeaveTypeId { get; set; }

    public int Priod { get; set; }
}