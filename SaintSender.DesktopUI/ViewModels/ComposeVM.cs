﻿using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using SaintSender.Core.Interfaces;
using SaintSender.Core.Services.Utils;

namespace SaintSender.Core.Services
{
    public class ComposeVM
    {
        private string toAddress;
        private string subject;
        private string message;
        private string messageToUser;
        private IEmailService _mailService;

        public bool canSend { get; private set; }

        public ComposeVM(IEmailService emailService)
        {
            _mailService = emailService;
        }

        public void setComposeVM(string to, string subject, string message)
        {
            toAddress = to;
            this.subject = subject;
            this.message = message;
            messageToUser = "Email sent succesfully";
            canSend = false;
        }
        private void Validation()
        {
            if (toAddress == string.Empty || !AddressValidator.IsValidEmailAddress(toAddress))
            {
                messageToUser = "Provide a correct email address...";
            }
            else if (subject == string.Empty)
            {
                messageToUser = "Do not leave subject empty...";
            }
            else if (message == string.Empty)
            {
                messageToUser = "Do not leave message empty...";
            }
            else
            {
                canSend = true;
            }
        }

        public bool Sending()
        {
            bool sent = false;
            Validation();
            if (canSend)
            {
                _mailService.SendMail(toAddress, subject, message);
                sent = true;
            }
            MessageBox.Show(messageToUser, "Status", MessageBoxButton.OK, MessageBoxImage.Information);
            return sent;
        }

        public bool CloseResult()
        {
            bool wantToClose = false;
            MessageBoxResult result = MessageBox.Show("Are you sure to discard the message?", "Clarification" ,MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    wantToClose = true;
                    break;
                case MessageBoxResult.No:
                    break;
            }
            return wantToClose;
        }
    }
}
