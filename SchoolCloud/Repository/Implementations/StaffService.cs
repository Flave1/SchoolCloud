using Microsoft.EntityFrameworkCore;
using SchoolCloud.Data;
using SchoolCloud.DomainObjects;
using SchoolCloud.Enum;
using SchoolCloud.Extensions;
using SchoolCloud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Repository.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly DataContext _dataContext;
        public StaffService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddStaffAsync(Staff staff)
        {
            await _dataContext.Staffs.AddAsync(staff);
            var saved = await _dataContext.SaveChangesAsync();
            return saved > 0;
        }

        public string GenerateStaffMembershipeId(int staffId)
        {
            return "STF" + Exts.AppendZero(staffId) + staffId + "/" + DateTime.Today.Year.ToString("YYYY").ToString().ToUpper();
        }

        public async Task<IEnumerable<Staff>> GetAllStaffAsync()
        {
            return await _dataContext.Staffs.ToListAsync();
        }

        public async Task<Staff> GetStaffByUserIdAsync(string userId)
        {
            return await _dataContext.Staffs.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Staff> GetStaffByStaffIdAsync(int StaffId)
        {
            return await _dataContext.Staffs.SingleOrDefaultAsync(x => x.StaffId == StaffId);
        }

        public async Task<Staff> GetStaffByStaffMembershipIdAsync(string StaffMemberShipId)
        {
            return await _dataContext.Staffs.SingleOrDefaultAsync(x => x.StaffMembeershipID == StaffMemberShipId);
        }

        public async Task<IEnumerable<Staff>> GetStaffByStatusAsync(int staffStatus)
        {
            return await _dataContext.Staffs.Where(x => x.Status == (StaffStatus)staffStatus).ToListAsync();
        }

        public  async Task<bool> UpdateStaffAsync(Staff staff)
        {
            var staffToUpdate = await _dataContext.Staffs.FindAsync(staff.StaffId);
            _dataContext.Entry(staffToUpdate).CurrentValues.SetValues(staff);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0; 
        }
    }
}
