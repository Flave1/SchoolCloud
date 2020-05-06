using Libraryhub.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolCloud.Contract.RequestObjs
{
    public class AddPortalSettingsRegObj
    { 
        public int StaffType { get; set; }
        public int Classes { get; set; }
        public int Staff { get; set; }
        public int Subjects { get; set; }
        public int Grades { get; set; }
        public bool IsCompleted { get; set; }
        public string SettupBy { get; set; }
    }

    public class EditPortalSettingsRegObj
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
    public class PortalSettingsObj
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

    public class PortalSettingsRegResObj
    {
        public int PortalSettingsId { get; set; }
        public APIResponseStatus Status { get; set; }
    }

    public class PortalSettingsResObj
    { 
        public List<PortalSettingsObj> PortalSettings { get; set; }
        public APIResponseStatus Status { get; set; }
    }
}
