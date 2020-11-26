using System;
using System.Collections.Generic;
using System.IO;
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
using SaintSender.Core.Interfaces;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;
        private IEmailService _emailService;


        public MainWindow(IEmailService emailService)
        {
            InitializeComponent();
            _emailService = emailService;
            _vm = new MainWindowViewModel(emailService);
            this.DataContext = _vm;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            var selectedEmail = (Mail)dg.SelectedItem;
            var attrDialog = new SingleMail(selectedEmail);
            attrDialog.Show();
        }

        private void ComposeBtn_Clicked(object sender, RoutedEventArgs e)
        {
            CompWindow compose = new CompWindow(_emailService);
            compose.Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.RefreshEmail();
            
        }
    }
}