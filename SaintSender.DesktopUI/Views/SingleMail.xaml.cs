using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SaintSender.Core.Entities;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for SingleMail.xaml
    /// </summary>
    public partial class SingleMail : Window
    {
        public SingleMail(Mail mail)
        {
            InitializeComponent();
            SingleMailViewModel vm = new SingleMailViewModel(mail);
            DataContext = vm;
        }
    }
}