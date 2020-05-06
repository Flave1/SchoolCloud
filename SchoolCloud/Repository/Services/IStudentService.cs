using SchoolCloud.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Repository.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentAsync();
        Task<IEnumerable<Student>> GetStudentsByStatusAsync(int studentStatus);
        Task<Student> GetStudentByUserIdAsync(string userId);
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<Student> GetStudentByStudentMembershipIdIdAsync(string studentId);
        Task<bool> AddStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(Student student);
        string GenerateStudentMembershipId(int studenttId); 
    }
}
