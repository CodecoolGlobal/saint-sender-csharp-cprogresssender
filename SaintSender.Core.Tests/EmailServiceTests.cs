using System.Security;
using NUnit.Framework;
using SaintSender.Core.Services;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    class EmailServiceTests
    {
        [Test]
        public void Fetch_Mails_Are_Received()
        {
            LoginService.UserAddress = "cprogresssender@gmail.com";
            var secure = new SecureString();
            foreach (char c in "CPSpi1000101")
            {
                secure.AppendChar(c);
            }
            LoginService.Password = secure;
            var fetcher = new EmailService();
            var mails = fetcher.GetEmails();
            Assert.NotNull(mails);
        }
    }
}
