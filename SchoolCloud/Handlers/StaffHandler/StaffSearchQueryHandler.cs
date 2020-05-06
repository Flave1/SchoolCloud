using AutoMapper;
using Libraryhub.Contracts.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using SchoolCloud.Contract.Queries;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.Contract.Response;
using SchoolCloud.DomainObjects;
using SchoolCloud.Enum;
using SchoolCloud.ErrorHandler;
using SchoolCloud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolCloud.Handlers.StaffHandler
{
    public class StaffSearchQueryHandler : IRequestHandler<StaffSearchQuery, StaffResObj>
    {
		private readonly IStaffService _staffService;
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		public StaffSearchQueryHandler(IStaffService staffService, ILoggerFactory loggerFactory, IMapper mapper)
		{
			_mapper = mapper;
			_staffService = staffService;
			_logger = loggerFactory.CreateLogger(typeof(StaffSearchQueryHandler));
		}
        public async Task<StaffResObj> Handle(StaffSearchQuery request, CancellationToken cancellationToken)
        {
			try
			{
				#region Search through staff list from repo 
				var StaffListFromRepo = await _staffService.GetAllStaffAsync();
				var searched = new List<Staff>();
				if(request.AllStaff > 0)
				{
					return new StaffResObj
					{
						Staff = StaffListFromRepo != null ? _mapper.Map<List<StaffObj>>(StaffListFromRepo) : null,
						Status = new APIResponseStatus
						{
							Message = new APIResponseMessage
							{
								FriendlyMessage = StaffListFromRepo != null ? null : "Search Complete !! No matching record found",
							}
						}
					};
				}
				if (request.Status > 0)
				{
					searched.AddRange(StaffListFromRepo.Where(x => x.Status == (StaffStatus)request.Status));
				}
				else if (request.ClassId > 0)
				{
					var selectedClassTeachers = StaffListFromRepo.SelectMany(x => x.ClassTeacher.Where(m => m.ClassId == request.ClassId)).Select(u => u.TeacherId);
					foreach (var staffId in selectedClassTeachers)
					{
						searched.AddRange(StaffListFromRepo.Where(t => t.StaffId == staffId));
					}
				}
				else if (request.StaffId > 0)
				{
					searched.AddRange(StaffListFromRepo.Where(t => t.StaffId == request.StaffId));
				}
				return new StaffResObj
				{
					Staff = searched != null ? _mapper.Map<List<StaffObj>>(searched) : null,
					Status = new APIResponseStatus
					{
						Message = new APIResponseMessage
						{
							FriendlyMessage = searched != null ? null : "Search Complete !! No matching record found",
						}
					}
				};
				#endregion
			}
            catch (Exception ex)
			{
				#region Log error 
				var errorCode = ErrorID.Generate(4);
				_logger.LogInformation($"Error ID : StaffSearchQueryHandler{errorCode} Ex : {ex?.Message ?? ex?.InnerException?.Message} ErrStack : {ex?.StackTrace}");
				return new StaffResObj();
				#endregion
			}
        }
    }
}
