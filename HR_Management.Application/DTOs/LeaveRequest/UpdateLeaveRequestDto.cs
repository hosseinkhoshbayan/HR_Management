using HR_Management.Application.DTOs.Common;
using System;

namespace HR_Management.Application.DTOs.LeaveRequest;

public class UpdateLeaveRequestDto:BaseDto,ILeaveRequestDto
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public long LeaveTypeId { get; set; }

    public string RequestComments { get; set; }

    public bool Cancelled { get; set; }
}