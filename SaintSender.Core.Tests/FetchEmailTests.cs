using EAGetMail;
using NUnit.Framework;
using SaintSender.Core.Services;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    class FetchEmailTests
    {
        [Test]
        public void Fetch_Mails_Are_Received()
        {
            var fetcher = new FetchEmail();
            fetcher.FetchMail();
            Assert.NotNull(fetcher.MailInfo);
        }
    }
}
