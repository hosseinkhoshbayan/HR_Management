using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories
{
    public class LeaveRequestRepository:GenericRepository<LeaveRequest>,ILeaveRequestRepository
    {
        private readonly CompanyManagementDbContext _context;

        public LeaveRequestRepository(CompanyManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(long Id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(t => t.LeaveType)
                .FirstOrDefaultAsync(i=>i.Id == Id);
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(t => t.LeaveType).ToListAsync();
            return leaveRequests;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved=approvalStatus;
            _context.Entry(leaveRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}