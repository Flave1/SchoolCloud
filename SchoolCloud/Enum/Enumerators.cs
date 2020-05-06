using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Enum
{
    public enum ClassStatus
    {
        Active = 1,
        InActive
    }
    public enum DefaultRoles
    {
        SuperAdmin = 1,
        Admin,
        Bursar,
        Registrar,
        Teacher,
        Student
    }
    public enum UserType
    {
        Student = 1,
        Staff
    }
    public enum StaffStatus
    {
        Active = 1, 
        InActive,
        Live,
        Suspended,
        Expelled,
        Dismember
    }

    public enum StudentStatus
    {
        Active = 1,
        InActive,
        Suspended,
        Expelled,
        Dismember,
        PassedOut
    }
}
