using SchoolCloud.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class Student
    { 
        public long StudentId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string StudentMembeershipID { get; set; }
        public string FristName  { get; set; }
        public string SurName  { get; set; }
        public string MiddleName  { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth  { get; set; }
        public string Sex  { get; set; }
        public StudentStatus Status { get; set; }
        public string Carnet  { get; set; }
        public string TelePhone  { get; set; }
        public string MobilePhone  { get; set; }
        public string Nationality  { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string LGA { get; set; }
        public string NearestBustop { get; set; }
        public string Observations  { get; set; }
        public int GradeParaleloId { get; set; }
        public string FatherName  { get; set; }
        public string MotherName  { get; set; }
        public string FatherPhone  { get; set; }
        public string MotherPhone  { get; set; }
        public string FatherProfession  { get; set; }
        public string MotherProfession  { get; set; }
        public string FatherPlaceOfWork { get; set; }
        public string MotherPlaceOfWork  { get; set; }
        public string LastNameFather  { get; set; }
        public string LastNameMother  { get; set; }
        public string GuardianHomeAddress  { get; set; }
        public int ClassId { get; set; } 
    }
}
