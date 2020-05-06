using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PortalServiceController : Controller 
    {
        private readonly IStaffService _staffService;
        public PortalServiceController(IStaffService staffService)
        {
            _staffService = staffService;
        }
       
    }
}
