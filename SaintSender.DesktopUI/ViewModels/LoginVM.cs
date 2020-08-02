using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    public class LoginVM
    {
        // ad-hoc solution to test connectability to gmail.
        public bool AccessToGmail()
        {
            var service = new EmailService();
            return (service.GetEmails() != null);
        }
    }
}
