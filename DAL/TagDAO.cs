using BO.Dtos;
using DAL.Databases;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TagDAO
    {
        private TagDAO() { }
        
        private static readonly object _instanceLock = new object();
        private static TagDAO? _instance;
        public static TagDAO Instance
        {
            get
            {
                lock(_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new TagDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<TagResponse> GetTags()
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var tags = context.Tags.ToList();
                if (!tags.Any())
                    throw new Exception("Empty!");
                var response = new List<TagResponse>();

                foreach (var item in tags)
                {
                    var mappedTag = new TagResponse();
                    mappedTag.TagId = item.TagId;
                    mappedTag.TagName = item.TagName;

                    response.Add(mappedTag);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public bool AddTagToNewsArticle(string newsArticleId, int tagId)
        {
            var result = false;
            try
            {
                using (var context = new FunewsManagementDbContext())
                {
                    // Load the NewsArticle entity
                    var newsArticle = context.NewsArticles
                        .Include(n => n.Tags)  // Include the Tags collection
                        .FirstOrDefault(n => n.NewsArticleId == newsArticleId);

                    if (newsArticle == null)
                    {
                        throw new Exception("NewsArticle not found.");
                    }
                    // Check if the tag already exists in the article's Tags collection
                    var existingTag = newsArticle.Tags.FirstOrDefault(t => t.TagId == tagId);
                    if (existingTag != null)
                        throw new Exception("This tag is already associated with the news article.");

                    // Load the Tag entity
                    var tag = context.Tags.FirstOrDefault(t => t.TagId == tagId);

                    if (tag == null)
                    {
                        throw new Exception("Tag not found.");
                    }

                    // Add the tag to the article's Tags collection
                    newsArticle.Tags.Add(tag);

                    // Save changes to the database
                    context.SaveChanges();
                    result = true;
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool RemoveTagOfNewsArticle(string newsArticleId, int tagId)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                var news = context.NewsArticles.Include(x => x.Tags)
                    .FirstOrDefault(x => x.NewsArticleId == newsArticleId);
                if (news is null)
                    throw new Exception("News does not exist!");
                // Find the Tag within the NewsArticle's Tags collection
                var tag = news.Tags.FirstOrDefault(t => t.TagId == tagId);
                if (tag is not null)
                {
                    // Remove the Tag from the NewsArticle's Tags collection
                    news.Tags.Remove(tag);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
