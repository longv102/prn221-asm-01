using BO.Dtos;
using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for CategoryManagementSubWindow.xaml
    /// </summary>
    public partial class CategoryManagementSubWindow : Window
    {
        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();

        public bool IsUpdate { get; set; }

        public CategoryDto? Request {  get; set; }

        public CategoryManagementSubWindow()
        {
            InitializeComponent();
            Loaded += CategoryManagementSubWindow_Loaded;
        }

        private void CategoryManagementSubWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsUpdate)
            {
                btnUpdate.Visibility = Visibility.Visible;
                btnCreate.Visibility = Visibility.Collapsed;

                lblCategoryId.Visibility = Visibility.Visible;
                txtCategoryId.Visibility = Visibility.Visible;

                txtCategoryId.Text = Request?.CategoryId.ToString();
                txtCategoryName.Text = Request?.CategoryName;
                txtCategoryDes.Text = Request?.CategoryDesciption;
            }
            else
            {
                btnUpdate.Visibility = Visibility.Collapsed;
                btnCreate.Visibility = Visibility.Visible;

                lblCategoryId.Visibility = Visibility.Collapsed;
                txtCategoryId.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult option = MessageBox.Show("Are you sure you want to create this account?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var category = new CategoryDto()
                    {
                        CategoryName = txtCategoryName.Text,
                        CategoryDesciption = txtCategoryDes.Text,
                    };
                    var result = _categoryRepository.CreateCategory(category);
                    if (result)
                        MessageBox.Show("Create successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBoxResult option = MessageBox.Show("Are you sure you want to update this account?",
                    "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var updateCategory = new CategoryDto()
                    {
                        CategoryId = short.Parse(txtCategoryId.Text),
                        CategoryName = txtCategoryName.Text,
                        CategoryDesciption = txtCategoryDes.Text,
                    };
                    var result = _categoryRepository.UpdateCategory(updateCategory);
                    if (result)
                        MessageBox.Show("Update successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
