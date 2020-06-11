using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    class MainWindowViewModel
    {
        private List<Mail> _mails;
        private readonly EmailService _emailService;

        public List<Mail> ListOfEMails
        {
            get => _mails;
            set => _mails = value;
        }

        public MainWindowViewModel()
        {
            _emailService = new EmailService();
            ListOfEMails = new List<Mail>(_emailService.GetEmails());
        }
    }
}