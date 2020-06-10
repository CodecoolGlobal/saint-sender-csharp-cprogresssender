﻿using System;
using System.Text.RegularExpressions;

namespace SaintSender.Core.Services
{
    public class ComposeVM
    {
        private string toAddress;
        private string subject;
        private string message;
        public string result { get; private set; }

        public ComposeVM(string to, string subject, string message)
        {
            toAddress = to;
            this.subject = subject;
            this.message = message;
            result = "Email sent succesfully";
        }
        public void Validation()
        {
            if (toAddress == string.Empty || !EmailService.IsValidEmailAddress(toAddress))
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
        }

        public void Sending()
        {
            if (result == "Email sent succesfully")
            {
                EmailService.SendMail(toAddress, subject, message);
            }
        }
    }
}
