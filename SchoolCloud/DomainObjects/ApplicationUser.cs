using Microsoft.AspNetCore.Identity;
using SchoolCloud.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.DomainObjects
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsFirstTimeLogin { get; set; }
        public UserType UserType { get; set; } 
        public string MembershipId { get; set; }
    }
}
