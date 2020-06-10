﻿using System;
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
using System.Windows.Shapes;
using SaintSender.Core.Services;
using SaintSender.Core.Interfaces;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for CompWindow.xaml
    /// </summary>
    public partial class CompWindow : Window
    {
        public CompWindow()
        {
            InitializeComponent();
        }

        private void Send_Clicked(object sender, RoutedEventArgs e)
        {
            string result = ValidatePreSend.IsSuccessful(targetEmail.Text, subject.Text, message.Text);
            if (result == "send")
            {
                EmailService.SendMail(targetEmail.Text, subject.Text, message.Text);
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void Close_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
