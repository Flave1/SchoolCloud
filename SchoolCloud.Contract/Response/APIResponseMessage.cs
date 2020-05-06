using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolCloud.Contract.Response
{


    public class APIResponseMessage
    {
        public string FriendlyMessage { get; set; }
        public string TechnicalMessage { get; set; }
        public string MessageId { get; set; }
        public string SearchResultMessage { get; set; }
        public string ShortErrorMessage { get; set; }
    }
}
