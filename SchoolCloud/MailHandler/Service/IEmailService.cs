using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolCloud.MailHandler.Service
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
