using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories
{
    public class LeaveAllocationRepository:GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly CompanyManagementDbContext _context;

        public LeaveAllocationRepository(CompanyManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
           var leaveAllocations = await _context.LeaveAllocations
               .Include(t=>t.LeaveType)
               .ToListAsync();
           return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(long Id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(t => t.LeaveType)
                .FirstOrDefaultAsync(i=>i.Id==Id);
            return leaveAllocation;
        }
    }
}