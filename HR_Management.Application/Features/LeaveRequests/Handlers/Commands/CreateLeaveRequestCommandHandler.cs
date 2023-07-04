﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management.Application.Contracts.Infrastructure;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Models;
using HR_Management.Application.Responses;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler: IRequestHandler<CreateLeaveRequestCommand,BaseCommandResponse>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
    }
    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        #region validation

        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

        if (validationResult.IsValid == false)
        {
            //throw new ValidationException(validationResult);
            response.Success = false;
            response.Message = "خطا در افزودن درخواست";
            response.Errors = validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
        }


        #endregion
        var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
       
        response.Success = true;
        response.Message = "عملیات با موفقیت انجام شد";
        response.Id= leaveRequest.Id;

        var email = new Email
        {
            To="khoshbayan1184@gmail.com",
            Subject = "Leave request submitted",
            Body = $"Your leave request for {request.LeaveRequestDto.StartDate}"+
                   $"To {request.LeaveRequestDto.EndDate}"+
                   $"has been submitted"
        };
        try
        {
            await _emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
           //log
        }

        return response;
    }
}