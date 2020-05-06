using AutoMapper;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.MapProfile
{
    public class DomainToRequest : Profile
    {
        public DomainToRequest()
        {
            CreateMap<Staff, StaffObj>();
        }
    }
}
