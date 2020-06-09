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
            var fetcher = new EmailService();
            var mails = fetcher.GetEmails();
            Assert.NotNull(mails);
        }
    }
}
