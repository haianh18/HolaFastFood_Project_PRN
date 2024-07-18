using HolaBase.Models;
using HolaBase.Service;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            txtEmail.Text = "admin";
            txtPass.Password = "admin123@";

            //txtEmail.Text = "staff_1";
            //txtPass.Password = "staff123@";
#endif

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Please input all fields", "Login");
                return;
            }
            AccountService accountService = new AccountService();
            Account account = accountService.Login(txtEmail.Text, txtPass.Password);
            if (isValidLogin(txtEmail.Text, txtPass.Password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.btnManageStore.Visibility = Visibility.Visible;
                mainWindow.txtAccountLogin.Content = account.DisplayName;
                mainWindow.Show();
                this.Close();
            }
            else if (account != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.btnManageStore.Visibility = Visibility.Hidden;
                mainWindow.txtAccountLogin.Content = account.DisplayName;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login Failed! UserName or Passwor incorrect", "Login");
            }
        }

        private bool isValidLogin(string username, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            var adminEmail = config["Admin:UserName"];
            var adminPass = config["Admin:Password"];
            if (adminEmail == username && adminPass == password)
            {
                return true;
            }
            return false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
