using BO.Constants;
using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public short UserId { get; set; }
        
        private ISystemAccountRepository _systemAccountRepository = new SystemAccountRepository();
        
        public ProfileWindow()
        {
            InitializeComponent();
            Loaded += ProfileWindow_Loaded;
        }

        private void ProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtAccountId.IsEnabled = false;
                txtAccountEmail.IsEnabled = false;
                txtAccountRole.IsEnabled= false;

                var account = _systemAccountRepository.GetAccountById(UserId);
                // Binding account's data into textboxes
                txtAccountId.Text = account.AccountId.ToString();
                txtAccountName.Text = account.AccountName;
                txtAccountEmail.Text = account.AccountEmail;
                if (account.AccountRole == AccountRoles.StaffRole)
                    txtAccountRole.Text = "Staff";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var role = txtAccountRole.Text;
                int roleValue = 0; 
                //TODO: Refactor these lines
                if ("Staff" == role) roleValue = 1;
                if ("Lecturer" == role) roleValue = 2;
                
                var account = new SystemAccountRequest()
                {
                    AccountId = UserId,
                    AccountEmail = txtAccountEmail.Text,
                    AccountName = txtAccountName.Text,
                    AccountRole = roleValue,
                };
                MessageBoxResult option = MessageBox.Show("Are you sure you want to update this account?", "Confirm Update", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var result = _systemAccountRepository.UpdateAccountForStaff(account);
                    if (result)
                        MessageBox.Show("Update successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
