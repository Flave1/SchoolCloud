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
    
    public class StudentService : IStudentService
    {
        private readonly DataContext _dataContext;
        public StudentService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddStudentAsync(Student student)
        {
            await _dataContext.Students.AddAsync(student);
            var saved = await _dataContext.SaveChangesAsync();
            return saved > 0;
        }

        public string GenerateStudentMembershipId(int studentId)
        {
            return "STU"+Exts.AppendZero(studentId)+ studentId + "/"+DateTime.Today.Year.ToString("YYYY").ToString().ToUpper();
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _dataContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByUserIdAsync(string userId)
        {
            return await _dataContext.Students.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Student> GetStudentByStudentMembershipIdIdAsync(string studentMembershipId)
        {
            return await _dataContext.Students.SingleOrDefaultAsync(x => x.StudentMembeershipID == studentMembershipId);
        }
        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _dataContext.Students.FindAsync(studentId);
        }
        public async Task<IEnumerable<Student>> GetStudentsByStatusAsync(int studentStatus)
        {
            return await _dataContext.Students.Where(x => x.Status == (StudentStatus)studentStatus).ToListAsync();
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var studt = await _dataContext.Students.FindAsync(Convert.ToInt64(student.StudentId)); 
            _dataContext.Entry(studt).CurrentValues.SetValues(student);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
         
    }
}
