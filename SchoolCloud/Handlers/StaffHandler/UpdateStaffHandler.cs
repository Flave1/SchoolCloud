using Libraryhub.Contracts.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using SchoolCloud.Contract.Commands;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.Contract.Response;
using SchoolCloud.ErrorHandler;
using SchoolCloud.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolCloud.Handlers.StaffHandler
{
    public class UpdateStaffHandler : IRequestHandler<UdateStaffCommand, StaffRegRespObj>
    {
		private readonly IStaffService _staffService;
		private readonly ILogger _logger;
		public UpdateStaffHandler(IStaffService staffService, ILogger logger)
		{
			_staffService = staffService;
			_logger = logger;
		}
        public async Task<StaffRegRespObj> Handle(UdateStaffCommand request, CancellationToken cancellationToken)
        {
			try
			{
				return null;
			}
			catch (Exception ex)
			{
				#region Log error 
				var errorCode = ErrorID.Generate(4);
				_logger.LogInformation($"Error ID : UpdateStaffHandler{errorCode} Ex : {ex?.Message ?? ex?.InnerException?.Message} ErrStack : {ex?.StackTrace}");
				return new StaffRegRespObj 
				{ 
					
					Status = new APIResponseStatus
					{
						Message = new APIResponseMessage
						{
							FriendlyMessage = "Error occured!! Please tyr again later",
							MessageId = errorCode, 
							TechnicalMessage = $"Error ID : UpdateStaffHandler{errorCode} Ex : {ex?.Message ?? ex?.InnerException?.Message} ErrStack : {ex?.StackTrace}"
						}
					}
				};
				#endregion
			}
		}
    }
}
