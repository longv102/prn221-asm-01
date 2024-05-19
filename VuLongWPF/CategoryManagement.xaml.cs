using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for CategoryManagement.xaml
    /// </summary>
    public partial class CategoryManagement : Window
    {
        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();

        public CategoryManagement()
        {
            InitializeComponent();
            Loaded += CategoryManagement_Loaded;
        }

        private void Clear()
        {
            txtCategoryId.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
            txtCategoryDes.Text = string.Empty;
        }

        private void LoadCategoryData()
        {
            var categories = _categoryRepository.GetCategories();
            dgCategories.ItemsSource = categories;
        }

        private void CategoryManagement_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtCategoryId.IsEnabled = false;
                txtCategoryName.IsReadOnly = true;
                txtCategoryDes.IsReadOnly = true;

                LoadCategoryData();
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
                CategoryManagementSubWindow subWindow = new CategoryManagementSubWindow()
                {
                    IsUpdate = false
                };
                subWindow.Show();
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
                var category = new CategoryDto()
                {
                    CategoryId = short.Parse(txtCategoryId.Text),
                    CategoryName = txtCategoryName.Text,
                    CategoryDesciption = txtCategoryDes.Text
                };

                CategoryManagementSubWindow subWindow = new CategoryManagementSubWindow()
                {
                    Request = category,
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
                MessageBoxResult option = MessageBox.Show("Are you sure you want to delete this account?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var categoryId = short.Parse(txtCategoryId.Text);
                    var result = _categoryRepository.DeleteCategory(categoryId);
                    if (result)
                        MessageBox.Show("Delete successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCategoryData();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgCategories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgCategories.SelectedItem is not null)
            {
                var selectedItem = dgCategories.SelectedItem as CategoryDto;
                // Bind these properties into textboxes
                txtCategoryId.Text = selectedItem?.CategoryId.ToString();
                txtCategoryName.Text = selectedItem?.CategoryName;
                txtCategoryDes.Text = selectedItem?.CategoryDesciption;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text;
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var categories = _categoryRepository.SearchCategoryByName(searchValue);
                    dgCategories.ItemsSource = categories;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCategoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
