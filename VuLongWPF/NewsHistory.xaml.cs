using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for NewsHistory.xaml
    /// </summary>
    public partial class NewsHistory : Window
    {
        public short UserId { get; set; }

        private readonly INewsRepository _newsRepository = new NewsRepository();
        
        public NewsHistory()
        {
            InitializeComponent();
            Loaded += NewsHistory_Loaded;
        }

        private void NewsHistory_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var news = _newsRepository.GetNewsByStaffId(UserId);
                dgNews.ItemsSource = news;
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
    }
}
