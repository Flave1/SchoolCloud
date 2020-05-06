using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class ClassTeacher
    {
        public int ClassTeacherId { get; set; } 
        public int TeacherId { get; set; }  
        public int ClassId { get; set; }
    }
}
