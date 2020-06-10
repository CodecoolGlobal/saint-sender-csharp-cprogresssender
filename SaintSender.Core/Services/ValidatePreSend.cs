using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class ValidatePreSend
    {
        public static string IsSuccessful(string to, string subject, string message)
        {
            string result = "send";
            if (to == string.Empty || !IsValidEmailAddress(to))
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
            return result;
        }

        public static bool IsValidEmailAddress(string to)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(to);
        }
    }
}
