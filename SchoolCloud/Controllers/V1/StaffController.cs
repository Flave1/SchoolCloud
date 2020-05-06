using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCloud.Contract.Queries;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.Contract.V1;
using SchoolCloud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Controllers.V1
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StaffController : Controller
    { 
        private readonly IMediator _mediator;
        public StaffController(IMediator mediator)
        { 
            _mediator = mediator; 
        }

        [HttpGet(ApiRoutes.Staff.SEARCH)]
        public async Task<ActionResult> StaffSearch(StaffSearchQuery query)
        {
            var response = await _mediator.Send(query); 
            return Ok(response); 
        }
    }
}
