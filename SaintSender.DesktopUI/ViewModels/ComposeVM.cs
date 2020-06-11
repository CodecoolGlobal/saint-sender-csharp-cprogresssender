using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace SaintSender.Core.Services
{
    public class ComposeVM
    {
        private string toAddress;
        private string subject;
        private string message;
        public string result { get; private set; }

        public ComposeVM()
        {

        }

        public ComposeVM(string to, string subject, string message)
        {
            toAddress = to;
            this.subject = subject;
            this.message = message;
            result = "Email sent succesfully";
        }
        private void Validation()
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
            Validation();
            if (result == "Email sent succesfully")
            {
                EmailService.SendMail(toAddress, subject, message);
            }
            MessageBox.Show(result);
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
