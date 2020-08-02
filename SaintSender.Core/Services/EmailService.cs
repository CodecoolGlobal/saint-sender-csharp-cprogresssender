using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security;
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
                LoginService.UserAddress, //"cprogresssender@gmail.com",
                SecureStringToString(LoginService.Password), //"CPSpi1000101",
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
                // changed _mails to null because this is the ad-hoc connection tester in LoginVM.
                _mails = null;
                // throw (e);
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

        public static void SendMail(string target, string subject, string message)
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
