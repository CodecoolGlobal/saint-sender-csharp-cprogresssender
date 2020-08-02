using System.Collections.Generic;
using SaintSender.Core.Entities;

namespace SaintSender.Core.Interfaces
{
    interface IEmailService
    {
        List<Mail> GetEmails();
    }
}
