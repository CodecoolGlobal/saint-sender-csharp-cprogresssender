using System.Windows;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.Core.Services;
using System.Windows.Controls;
using System.Configuration;
using System.Security;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginVM loginVM;
        public LoginWindow()
        {
            InitializeComponent();
            LoadUserCredentials();
            loginVM = new LoginVM(); 
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginService.UserAddress = userMailAddress.Text;
            LoginService.Password =  passwordBox1.SecurePassword;

            if (string.IsNullOrEmpty(userMailAddress.Text) || string.IsNullOrWhiteSpace(passwordBox1.Password))
            {
                appMessage.Content = "Please fill out both fields.";
            }
            else
            {
                if (loginVM.AccessToGmail())
                {
                    SaveUserCredentials(userMailAddress.Text, passwordBox1.SecurePassword);
                    this.DialogResult = true;
                    this.Close();
                }
                appMessage.Content = "Login was not successful.";
            }
        }

        private void SaveUserCredentials(string userName, SecureString password)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["UserName"].Value = userName.Trim();
            //config.AppSettings.Settings["UserPassword"].Value = password;
            ConfigurationManager.RefreshSection("appSettings");
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void LoadUserCredentials()
        {
            userMailAddress.Text = ConfigurationManager.AppSettings.Get("UserName");
        }
    }
}
