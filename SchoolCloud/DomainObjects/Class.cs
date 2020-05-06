using SchoolCloud.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { set; get; }
        public string TrimeSterId { get; set; }
        public string Year { get; set; }
        public ClassStatus Status { get; set; }
        public string FormTeacherId { get; set; } 
        public List<ClassTeacher> ClassTeachers { get; set; }
        public List<Student> Students { get; set; }
    }
}