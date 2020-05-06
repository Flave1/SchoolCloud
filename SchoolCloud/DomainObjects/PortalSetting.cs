using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class PortalSetting
    {
        public int PortalSettingsId { get; set; }
        public int  StaffType { get; set; }
        public int Classes { get; set; }
        public int Staff { get; set; }
        public int Subjects { get; set; }
        public int Grades  { get; set; }
        public bool IsCompleted  { get; set; }
        public string SettupBy { get; set; }
    }
}
