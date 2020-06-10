using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class Sender
    {
        private string toAddress;
        private string subject;
        private string message;
        public string result { get; private set; }

        public Sender(string to, string subject, string message)
        {
            this.toAddress = to;
            this.subject = subject;
            this.message = message;
            this.result = "Email sent succesfully";
        }
        public void Validation()
        {
            if (toAddress == string.Empty || !IsValidEmailAddress(toAddress))
            {
                result = "Provide a correct email address...";
            }
            else if (subject == string.Empty)
            {
                result = "Do not leave subject empty...";
            }
            else if (message == string.Empty)
            {
                result = "Do not leave message empty...";
            }
        }

        public void Sending()
        {
            if (result == "Email sent succesfully")
            {
                EmailService.SendMail(toAddress, subject, message);
            }
        }

        public static bool IsValidEmailAddress(string to)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(to);
        }
    }
}
