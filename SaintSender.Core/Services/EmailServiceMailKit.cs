using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security;
using SaintSender.Core.Interfaces;
using System.Net;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MailKit.Search;

namespace SaintSender.Core.Services
{
    public class EmailServiceMailKit: IEmailService
    {
        private List<Entities.Mail> _mails = new List<Entities.Mail>();

       
        private void FetchMail()
        {

            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                client.Authenticate(
                    LoginService.UserAddress,
                    SecureStringToString(LoginService.Password));

                ReceiveEmails(client);
                client.Disconnect(true);
            }
        }

        private void ReceiveEmails(ImapClient client)
        {
            client.Inbox.Open(FolderAccess.ReadOnly);
            var mailIds = client.Inbox.Search(SearchQuery.All);
            foreach (var id in mailIds)
            {
                var mail = client.Inbox.GetMessage(id);
                var info = client.Inbox.Fetch(new[] { id }, MessageSummaryItems.Flags);
                _mails.Add(new Entities.Mail(
                    mail.From.ToString(),
                    mail.Subject.ToString(),
                    mail.TextBody.ToString(),
                    mail.Date.DateTime,
                    info[0].Flags.Value.HasFlag(MessageFlags.Seen)
                    ));
            }

        }
        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public List<Entities.Mail> GetEmails()
        {
            _mails.Clear();
            FetchMail();
            return _mails;
        }

        public void SendMail(string target, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                // TODO: change From to the logged in account, and also it's pw
                mail.From = new System.Net.Mail.MailAddress(LoginService.UserAddress);
                mail.To.Add(target);
                mail.Subject = subject;
                mail.Body = message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(LoginService.UserAddress, LoginService.Password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public static bool IsValidEmailAddress(string to)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(to);
        }
    }
}
