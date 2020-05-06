using MediatR;
using SchoolCloud.Contract.RequestObjs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolCloud.Contract.Commands
{
    public class AddPortalSettingsCommand : IRequest<PortalSettingsResObj>
    {
        public int StaffType { get; set; }
        public int Classes { get; set; }
        public int Staff { get; set; }
        public int Subjects { get; set; }
        public int Grades { get; set; }
        public bool IsCompleted { get; set; }
        public string SettupBy { get; set; }
    }

    public class EditPortalSettingsCommand : IRequest<PortalSettingsResObj>
    {
        public int PortalSettingsId { get; set; }
        public int StaffType { get; set; }
        public int Classes { get; set; }
        public int Staff { get; set; }
        public int Subjects { get; set; }
        public int Grades { get; set; }
        public bool IsCompleted { get; set; }
        public string SettupBy { get; set; }
    }
}
