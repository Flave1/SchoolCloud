using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class ScoreRecord
    { 
        public long ScoreRecordId { get; set; }
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        public double Assignment { get; set; }
        public double Tesst { get; set; }
        public double Examination { get; set; }
        public double FinalGrade { get; set; }
        public string TrimeStar { get; set; }
        public string Year { get; set; }
        public int FormTeacherId { get; set; }
        public int ClassId { get; set; }

    }
}
