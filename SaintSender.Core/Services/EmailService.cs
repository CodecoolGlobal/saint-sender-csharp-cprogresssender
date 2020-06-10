using System;
using System.Collections.Generic;
using System.Net.Mail;
using EAGetMail;
using SaintSender.Core.Interfaces;

namespace SaintSender.Core.Services
{
    public class EmailService: IEmailService
    {
        private List<Entities.Mail> _mails = new List<Entities.Mail>();
        public MailInfo[] MailInfo { get; set; }

        private void FetchMail()
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
                ReceiveEmails(oClient);
                oClient.Quit();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        private void ReceiveEmails(MailClient client)
        {
            MailInfo[] mailInfos = client.GetMailInfos();
            foreach (var mailInfo in mailInfos)
            {
                Mail oMail = client.GetMail(mailInfo);
                _mails.Add(new Entities.Mail(
                    oMail.From.ToString(),
                    oMail.Subject.ToString(),
                    oMail.TextBody,
                    oMail.SentDate,
                    mailInfo.Read
                    ));
            }

        }

        public List<Entities.Mail> GetEmails()
        {
            FetchMail();
            return _mails;
        }

        public static void SendMail(string target, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                // TODO: change From to the logged in account, and also it's pw
                mail.From = new System.Net.Mail.MailAddress("cprogresssender@gmail.com");
                mail.To.Add(target);
                mail.Subject = subject;
                mail.Body = message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cprogresssender@gmail.com", "CPSpi1000101");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
