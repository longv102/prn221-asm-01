using BO.Constants;
using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Text.RegularExpressions;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for AdminSubWindow.xaml
    /// </summary>
    public partial class AdminSubWindow : Window
    {
        private List<Role> roles = new()
        {
            new Role
            {
                RoleName = "Staff",
                Value = 1
            },
            new Role
            {
                RoleName = "Lecturer",
                Value = 2
            },
        };
        
        // Get the selected account in admin window
        // Update function
        public SystemAccountRequest? Request { get; set; }

        public bool IsUpdate { get; set; }

        private ISystemAccountRepository _systemAccountRepository = new SystemAccountRepository();

        public AdminSubWindow()
        {
            InitializeComponent();
            Loaded += AdminSubWindow_Loaded;
            //btnUpdate.Visibility = Visibility.Collapsed;
            // Combobox for user role
            cboRole.ItemsSource = roles;
            cboRole.DisplayMemberPath = "RoleName";
            cboRole.SelectedValuePath = "Value";
        }

        private void AdminSubWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsUpdate)
            {
                btnUpdate.Visibility = Visibility.Visible;
                btnCreate.Visibility = Visibility.Collapsed;

                txtAccountId.IsEnabled = false;
                txtAccountId.Text = Request?.AccountId.ToString();
                txtAccountName.Text = Request?.AccountName;
                txtAccountEmail.Text = Request?.AccountEmail;
                cboRole.SelectedValue = Request?.AccountRole;
            }
            else
            {
                btnUpdate.Visibility = Visibility.Collapsed;
                btnCreate.Visibility = Visibility.Visible;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult option = MessageBox.Show("Are you sure you want to update this account?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var account = new SystemAccountRequest()
                    {
                        AccountId = short.Parse(txtAccountId.Text),
                        AccountName = txtAccountName.Text,
                        AccountEmail = txtAccountEmail.Text,
                        AccountRole = (int)cboRole.SelectedValue,
                    };
                    var result = _systemAccountRepository.UpdateAccount(account);
                    if (result)
                        MessageBox.Show("Update successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult option = MessageBox.Show("Are you sure you want to create this account?", "Confirm Create", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var account = new SystemAccountRequest()
                    {
                        AccountId = short.Parse(txtAccountId.Text),
                        AccountName = txtAccountName.Text,
                        AccountEmail = txtAccountEmail.Text,
                        AccountRole = (int)cboRole.SelectedValue
                    };
                    var result = _systemAccountRepository.CreateAccount(account);
                    if (result)
                        MessageBox.Show("Create successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtAccountId_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void txtAccountEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }
    }
}
