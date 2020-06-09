using System;
using System.Collections.Generic;
using EAGetMail;

namespace SaintSender.Core.Services
{
    public class FetchEmail
    {
        public MailInfo[] MailInfo { get; set; }

        public void FetchMail()
        {
            MailServer oServer = new MailServer("imap.gmail.com",
                "cprogresssender@gmail.com",
                "CPSpi1000101",
                ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");
            oServer.SSLConnection = true;
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);
                MailInfo = oClient.GetMailInfos();
                oClient.Quit();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
