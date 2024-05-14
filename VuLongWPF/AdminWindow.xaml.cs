using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly ISystemAccountRepository _systemAccountRepository = new SystemAccountRepository();

        public AdminWindow()
        {
            InitializeComponent();
            txtAccountId.IsEnabled = false;
        }

        private void LoadAccountData()
        {
            var accounts = _systemAccountRepository.GetAccounts();
            dgAccounts.ItemsSource = accounts;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadAccountData();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            AdminSubWindow subWindow = new AdminSubWindow();
            subWindow.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var account = new SystemAccountRequest()
                {
                    AccountId = short.Parse(txtAccountId.Text),
                    AccountEmail = txtAccountEmail.Text,
                    AccountName = txtAccountName.Text,
                    AccountRole = int.Parse(txtAccountRole.Text),
                };
                AdminSubWindow subWindow = new()
                {
                    Request = account,
                    IsUpdate = true
                };
                subWindow.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult option = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var accountId = short.Parse(txtAccountId.Text);
                    var result = _systemAccountRepository.DeleteAccount(accountId);
                    if (result)
                    {
                        MessageBox.Show("Delete successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Load account data again after delete action
                        LoadAccountData();
                        Clear();
                    }
                }
                else
                {
                    // Do nothing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear()
        {
            txtAccountId.Text = string.Empty;
            txtAccountEmail.Text = string.Empty;
            txtAccountName.Text = string.Empty;
            txtAccountRole.Text = string.Empty;
        }

        private void dgAccounts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not null)
            {
                var selectedItem = dgAccounts.SelectedItem as SystemAccountResponse;
                // Bind these properties into textboxes
                txtAccountId.Text = selectedItem.AccountId.ToString();
                txtAccountName.Text = selectedItem.AccountName;
                txtAccountEmail.Text = selectedItem.AccountEmail;
                txtAccountRole.Text = selectedItem.AccountRole.ToString();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var searchValue = txtSearchName.Text;
                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    var result = _systemAccountRepository.SearchAccountByName(searchValue);
                    dgAccounts.ItemsSource = result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
