using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int LevelId { get; set; }
        public int TrimeStar { get; set; }
        public int Attended { get; set; }
        public int Date { get; set; }
        public int FormTeacherId { get; set; }
    }
}
