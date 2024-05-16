using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public short CurrentUserId { get; set; }

        public StaffWindow()
        {
            InitializeComponent();
            
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryManagement categoryManagement = new CategoryManagement();
            categoryManagement.Show();
        }

        private void btnNews_Click(object sender, RoutedEventArgs e)
        {
            NewsManagement window = new NewsManagement()
            {
                UserId = CurrentUserId,
            };
            window.Show();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow()
            {
                UserId = CurrentUserId,
            };
            profileWindow.Show();
        }

        private void btnViewNews_Click(object sender, RoutedEventArgs e)
        {
            NewsHistory newsHistory = new NewsHistory()
            {
                UserId = CurrentUserId
            };
            newsHistory.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
