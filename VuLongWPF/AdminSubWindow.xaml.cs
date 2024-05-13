using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for AdminSubWindow.xaml
    /// </summary>
    public partial class AdminSubWindow : Window
    {
        public int MyProperty { get; set; }

        private ISystemAccountRepository _systemAccountRepository = new SystemAccountRepository();

        public AdminSubWindow()
        {
            InitializeComponent();
            btnUpdate.Visibility = Visibility.Collapsed;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate.Visibility = Visibility.Visible;
            btnCreate.Visibility = Visibility.Collapsed;


        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
