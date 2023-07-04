using HR_Management.Application.DTOs.Common;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Domain;
using System;

namespace HR_Management.Application.DTOs.LeaveRequest;

public class LeaveRequestDto : BaseDto,ILeaveRequestDto
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public long LeaveTypeId { get; set; }

    public DateTime DateRequested { get; set; }

    public string RequestComments { get; set; }

    public DateTime? DateAction { get; set; }

    public bool? Approved { get; set; }

    public bool Cancelled { get; set; }
}