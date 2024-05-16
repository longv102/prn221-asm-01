using Repositories;
using Repositories.Contracts;
using System.Windows;

namespace VuLongWPF
{
    /// <summary>
    /// Interaction logic for ViewTag.xaml
    /// </summary>
    public partial class ViewTag : Window
    {
        public string NewsArticleId { get; set; } = null!;

        private readonly INewsRepository _newsRepository = new NewsRepository();

        private readonly ITagRepository _tagRepository = new TagRepository();

        public ViewTag()
        {
            InitializeComponent();
            Loaded += ViewTag_Loaded;
        }

        private void Load()
        {
            cboTag.ItemsSource = _tagRepository.GetTags();
            cboTag.DisplayMemberPath = "TagName";
            cboTag.SelectedValuePath = "TagId";

            var tags = _newsRepository.ViewTagByNewsArticleId(NewsArticleId);
            dgTags.ItemsSource = tags;
        }

        private void ViewTag_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load tags of a news article
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult option = MessageBox.Show("Are you sure you want to add tag for this news?", 
                    "Confirm Add", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (option == MessageBoxResult.Yes)
                {
                    var tagValue = (int)cboTag.SelectedValue;
                    var result = _tagRepository.AddTag(NewsArticleId, tagValue);
                    if (result)
                        MessageBox.Show("Add successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
