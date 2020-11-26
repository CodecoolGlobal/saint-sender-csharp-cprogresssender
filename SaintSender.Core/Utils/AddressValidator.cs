using System.Text.RegularExpressions;

namespace SaintSender.Core.Services.Utils
{
    public class AddressValidator
    {
        public static bool IsValidEmailAddress(string to)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(to);
        }
    }
}