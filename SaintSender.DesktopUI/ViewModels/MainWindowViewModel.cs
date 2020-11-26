using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.Core.Interfaces;

namespace SaintSender.DesktopUI.ViewModels
{
    class MainWindowViewModel
    {
        private readonly IEmailService _emailService;

        public ObservableCollection<Mail> ListOfEMails { get; set; }


        public MainWindowViewModel(IEmailService mailService)
        {
            _emailService = mailService;
            ListOfEMails = new ObservableCollection<Mail>(_emailService.GetEmails());
        }

        public void RefreshEmail()
        {
            var mails = _emailService.GetEmails();
            if (mails != null)
            {
                ListOfEMails.Clear();
                mails.ForEach(x => ListOfEMails.Add(x));
            }

           
        }
    }
}