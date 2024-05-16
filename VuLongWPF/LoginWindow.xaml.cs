using BO.Constants;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Contracts;
using System.IO;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ISystemAccountRepository _systemAccountRepository = new SystemAccountRepository();
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Read admin credentials from appsettings.json
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                var adminEmail = configuration["FuNewsSystemAccount:Email"] ?? string.Empty;
                var adminPassword = configuration["FuNewsSystemAccount:Password"] ?? string.Empty;

                // Retrieve from login window textboxes
                var email = txtEmail.Text;
                var password = txtPassword.Password;

                // Authenticate for admin 
                if (email.Trim().ToLower() == adminEmail.Trim().ToLower() && password.Trim().ToLower() == adminPassword.Trim().ToLower())
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    Hide();
                }
                else
                {
                    var account = _systemAccountRepository.Authenticate(email, password);
                    if (account.AccountRole == AccountRoles.StaffRole)
                    {
                        // Navigate to staff window
                        StaffWindow staffWindow = new StaffWindow()
                        {
                            CurrentUserId = account.AccountId,
                        };
                        staffWindow.Show();
                        Hide();
                    }
                    else if (account.AccountRole == AccountRoles.LecturerRole)
                    {
                        MessageBox.Show("Your role is not supported!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
