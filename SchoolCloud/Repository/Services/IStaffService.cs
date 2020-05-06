using SchoolCloud.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Repository.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllStaffAsync();
        Task<IEnumerable<Staff>> GetStaffByStatusAsync(int StaffStatus);
        Task<Staff> GetStaffByUserIdAsync(string userId);
        Task<Staff> GetStaffByStaffIdAsync(int StaffId);
        Task<Staff> GetStaffByStaffMembershipIdAsync(string StaffMemberShipId);
        Task<bool> AddStaffAsync(Staff Staff);
        Task<bool> UpdateStaffAsync(Staff Staff);
        string GenerateStaffMembershipeId(int staffId);
    }
}
