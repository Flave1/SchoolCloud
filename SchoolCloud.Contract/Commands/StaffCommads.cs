using MediatR;
using SchoolCloud.Contract.RequestObjs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolCloud.Contract.Commands
{
    public class UdateStaffReqCommand : IRequest<StaffReqResObj>
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string StaffMembeershipID { get; set; }
        public string FristName { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Sex { get; set; }
        public string Carnet { get; set; }
        public string TelePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Nationality { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string LGA { get; set; }
        public string NearestBustop { get; set; }
        public DateTime DateHired { get; set; }
        public int YearsOfService { get; set; }
        public int Status { get; set; }
        public string Observations { get; set; }
        public string Specialty { get; set; }
        public int Salary { get; set; }
        public int StaffTypeId { get; set; }
        public int CategoryId { get; set; }
        public string GradeParaleloId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherPhone { get; set; }
        public string MotherPhone { get; set; }
    }

}
