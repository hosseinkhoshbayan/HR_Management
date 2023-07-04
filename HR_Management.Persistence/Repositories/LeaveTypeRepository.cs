using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;

namespace HR_Management.Persistence.Repositories
{
    public class LeaveTypeRepository:GenericRepository<LeaveType>,ILeaveTypeRepository
    {
        private readonly CompanyManagementDbContext _context;

        public LeaveTypeRepository(CompanyManagementDbContext context) : base(context)
        {
            _context = context;
        }
    }
}