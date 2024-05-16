using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for NewsManagementSubWindow.xaml
    /// </summary>
    public partial class NewsManagementSubWindow : Window
    {
        public bool IsUpdate { get; set; }

        public short UserId { get; set; }

        public NewsArticleRequest Request { get; set; } = null!;

        private readonly ICategoryRepository _categoryRepository;
        
        private readonly INewsRepository _newsRepository;
        
        public NewsManagementSubWindow()
        {
            InitializeComponent();
            _categoryRepository = new CategoryRepository();
            _newsRepository = new NewsRepository();
            Loaded += NewsManagementSubWindow_Loaded;
        }

        private void NewsManagementSubWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var categories = _categoryRepository.GetCategories();
            cboCategory.ItemsSource = categories;
            cboCategory.DisplayMemberPath = "CategoryName";
            cboCategory.SelectedValuePath = "CategoryId";
            // Set the value of date picker
            dpDate.SelectedDate = DateTime.Now;

            if (IsUpdate)
            {
                btnCreate.Visibility = Visibility.Collapsed;
                btnUpdate.Visibility = Visibility.Visible;
                txtArticleId.IsEnabled = false;
            }
            else
            {
                btnCreate.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Collapsed;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new NewsArticleRequest()
                {
                    NewsArticleId = txtArticleId.Text,
                    NewsTitle = txtTitle.Text,
                    NewsContent = txtContent.Text,
                    CategoryId = (short)cboCategory.SelectedValue,
                    ModifiedDate = dpDate.SelectedDate,
                    CreatedById = UserId
                };
                MessageBoxResult option = MessageBox.Show("Update the news?", "Confirm Create",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (option == MessageBoxResult.Yes)
                {
                    var result = _newsRepository.UpdateNews(request);
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
                var request = new NewsArticleRequest()
                {
                    NewsArticleId = txtArticleId.Text,
                    NewsTitle = txtTitle.Text,
                    NewsContent = txtContent.Text,
                    CategoryId = (short)cboCategory.SelectedValue,
                    CreatedDate = dpDate.SelectedDate,
                    CreatedById = UserId
                };
                MessageBoxResult option = MessageBox.Show("Create the news?", "Confirm Create", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (option == MessageBoxResult.Yes)
                {
                    var result = _newsRepository.CreateNews(request);
                    if (result)
                        MessageBox.Show("Create successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
