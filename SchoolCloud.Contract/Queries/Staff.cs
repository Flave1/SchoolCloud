using MediatR;
using SchoolCloud.Contract.RequestObjs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolCloud.Contract.Queries
{
    public class StaffSearchQuery :IRequest<StaffResObj>
    {
        public int StaffId { get; set; }
        public int Status { get; set; } 
        public int ClassId { get; set; }
        public int AllStaff { get; set; }
    } 
}
