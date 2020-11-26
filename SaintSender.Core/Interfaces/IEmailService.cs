using System.Collections.Generic;
using SaintSender.Core.Entities;

namespace SaintSender.Core.Interfaces
{
    public interface IEmailService
    {
        List<Mail> GetEmails();
        void SendMail(string target, string subject, string message);
    }
}
