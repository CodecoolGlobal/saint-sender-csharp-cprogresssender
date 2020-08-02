using System;

namespace SaintSender.Core.Entities
{
    public class Mail
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }

        public string MessageWithoutCrlf
        {
            get
            { 
                var msg = Message.Replace("\r\n", " ").Trim();
                return msg.Substring(0, Math.Min(msg.Length, 100)); 
            }
        }

        public Mail(string sender, string subject, string message, DateTime date, bool read)
        {
            Sender = sender;
            Subject = subject;
            Message = message;
            Date = date;
            Read = read;
        }

    }
}