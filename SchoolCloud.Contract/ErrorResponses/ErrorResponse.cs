using System;
using System.Collections.Generic;

namespace SchoolCloud.Contract.ErrorResponses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

    }
}
