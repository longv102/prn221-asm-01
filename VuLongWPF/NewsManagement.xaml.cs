using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for NewsManagement.xaml
    /// </summary>
    public partial class NewsManagement : Window
    {
        private readonly INewsRepository _newsRepository = new NewsRepository();

        public short UserId { get; set; }

        public NewsManagement()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadNewsData()
        {
            var response = _newsRepository.GetNews();
            dgNews.ItemsSource = response;
        }

        private void Clear()
        {
            txtArticleId.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
            txtCreatedDate.Text = string.Empty;
            txtModifiedDate.Text = string.Empty;
            txtContent.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtCreatedBy.Text = string.Empty;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadNewsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            NewsManagementSubWindow window = new NewsManagementSubWindow()
            {
                IsUpdate = false,
                UserId = UserId,
            };
            window.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult option = MessageBox.Show("Are you sure you want to delete this news?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var newsId = txtArticleId.Text;
                    var result = _newsRepository.DeleteNews(newsId);
                    if (result) MessageBox.Show("Delete successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadNewsData();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewsManagementSubWindow window = new NewsManagementSubWindow()
                {
                    IsUpdate = true,

                    UserId = UserId,
                };
                window.Show();

            }
            catch
            {

            }
        }

        private void dgNews_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgNews.SelectedItem is not null)
            {
                var selectedItem = dgNews.SelectedItem as NewsArticleResponse;
                // Bind these properties into textboxes
                txtArticleId.Text = selectedItem?.NewsArticleId;
                txtTitle.Text = selectedItem?.NewsTitle;
                txtCreatedDate.Text = selectedItem?.CreatedDate.ToString();
                txtContent.Text = selectedItem?.NewsContent;
                txtModifiedDate.Text = selectedItem?.ModifiedDate.ToString();
                txtCategoryName.Text = selectedItem?.CategoryName;
                txtCreatedBy.Text = selectedItem?.CreatedBy;
            }
        }

        private void btnTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewTag viewTagWindow = new ViewTag()
                {
                    NewsArticleId = txtArticleId.Text,
                };
                viewTagWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
