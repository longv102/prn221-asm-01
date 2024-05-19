using BO.Dtos;
using DAL.Databases;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class NewsArticleDAO
    {
        private NewsArticleDAO() { }

        private static readonly object _instanceLock = new object();

        private static NewsArticleDAO? _instance;
        public static NewsArticleDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new NewsArticleDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<NewsArticleResponse> GetNewsByStaffId(short id)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var response = new List<NewsArticleResponse>();

                var news = context.NewsArticles.Include(x => x.Category)
                    .Where(x => x.CreatedById == id && x.NewsStatus == true)
                    .ToList();
                if (!news.Any())
                    throw new Exception("News is not found!");
                foreach (var item in news)
                {
                    var mappedNews = new NewsArticleResponse();

                    mappedNews.NewsArticleId = item.NewsArticleId;
                    mappedNews.NewsTitle = item.NewsTitle;
                    mappedNews.CreatedDate = item.CreatedDate;
                    mappedNews.NewsContent = item.NewsContent;
                    mappedNews.CategoryName = item.Category?.CategoryName;
                    mappedNews.ModifiedDate = item.ModifiedDate;
                    response.Add(mappedNews);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<NewsArticleResponse> GetNews()
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var response = new List<NewsArticleResponse>();

                var news = context.NewsArticles
                    .Include(x => x.Category)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.NewsStatus == true)
                    .ToList();
                if (!news.Any())
                    throw new Exception("News is not found!");
                foreach (var item in news)
                {
                    var mappedNews = new NewsArticleResponse();

                    mappedNews.NewsArticleId = item.NewsArticleId;
                    mappedNews.NewsTitle = item.NewsTitle;
                    mappedNews.CreatedDate = item.CreatedDate;
                    mappedNews.NewsContent = item.NewsContent;
                    mappedNews.CategoryName = item.Category?.CategoryName;
                    mappedNews.ModifiedDate = item.ModifiedDate;
                    mappedNews.CreatedBy = item.CreatedBy?.AccountName;

                    response.Add(mappedNews);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TagResponse> GetTagByNewsId(string id)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var response = context.NewsArticles
                    .Where(x => x.NewsArticleId == id)
                    .SelectMany(x => x.Tags)
                    .Select(tag => new TagResponse
                    {
                        TagId = tag.TagId,
                        TagName = tag.TagName
                    })
                .ToList();
                if (!response.Any())
                    throw new Exception("Post has no tag!!!");
                return response;
            }
            catch
            {
                throw;
            }
        }

        public NewsArticleResponse GetNewsById(string id)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var news = context.NewsArticles
                    .Include(x => x.Category)
                    .FirstOrDefault(x => x.NewsArticleId == id);
                if (news is null)
                    throw new Exception("News does not exist!");
                var response = new NewsArticleResponse()
                {
                    NewsArticleId = news.NewsArticleId,
                    NewsTitle = news.NewsTitle,
                    NewsContent = news.NewsContent,
                    CategoryName = news.Category?.CategoryName,
                };
                return response;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteNews(string id)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                var news = context.NewsArticles.Find(id);
                if (news is null)
                    throw new Exception("News does not exist!");
                context.NewsArticles.Remove(news);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool CreateNews(NewsArticleRequest request)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                if (string.IsNullOrEmpty(request.NewsArticleId))
                    throw new Exception("Article id is required!");
                if (string.IsNullOrEmpty(request.NewsTitle))
                    throw new Exception("Article title is required!");
                if (string.IsNullOrEmpty(request.NewsContent))
                    throw new Exception("Article content is required!");
                
                // check duplicate of news article
                var existedNews = context.NewsArticles.Any(x => x.NewsArticleId == request.NewsArticleId);
                if (existedNews)
                    throw new Exception("This article id has already existed!");
                var news = new NewsArticle()
                {
                    NewsArticleId = request.NewsArticleId,
                    NewsTitle = request.NewsTitle,
                    NewsContent = request.NewsContent,
                    CategoryId = request.CategoryId,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    // Hard-code for news status
                    NewsStatus = true, 
                };
                context.NewsArticles.Add(news);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool UpdateNews(NewsArticleRequest request)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                if (string.IsNullOrEmpty(request.NewsTitle))
                    throw new Exception("Article title is required!");
                if (string.IsNullOrEmpty(request.NewsContent))
                    throw new Exception("Article content is required!");

                var existedNews = context.NewsArticles.FirstOrDefault(x => x.NewsArticleId == request.NewsArticleId);
                if (existedNews is null)
                    throw new Exception("News article does not exist!");
                // Update fields
                existedNews.NewsTitle = request.NewsTitle;
                existedNews.NewsContent = request.NewsContent;
                existedNews.CategoryId = request.CategoryId;
                existedNews.ModifiedDate = request.ModifiedDate;
                
                context.NewsArticles.Update(existedNews);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public IEnumerable<NewsArticleResponse> GetNewsByTitle(string title)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var news = context.NewsArticles
                    .Include(x => x.Category)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.NewsTitle.Contains(title))
                    .ToList();
                if (!news.Any())
                    throw new Exception("News does not exist!");
                
                var response = new List<NewsArticleResponse>();
                foreach (var item in news)
                {
                    var mappedNews = new NewsArticleResponse();
                    
                    mappedNews.NewsArticleId = item.NewsArticleId;
                    mappedNews.NewsTitle = item.NewsTitle;
                    mappedNews.NewsContent = item.NewsContent;
                    mappedNews.CategoryName = item.Category?.CategoryName;
                    mappedNews.CreatedDate = item.CreatedDate;
                    mappedNews.CreatedBy = item.CreatedBy?.AccountName;
                    mappedNews.ModifiedDate = item.ModifiedDate;
                    response.Add(mappedNews);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<NewsArticleResponse> GetNewsByContent(string content)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var news = context.NewsArticles
                    .Include(x => x.Category)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.NewsContent.Contains(content))
                    .ToList();
                if (!news.Any())
                    throw new Exception("News does not exist!");

                var response = new List<NewsArticleResponse>();
                foreach (var item in news)
                {
                    var mappedNews = new NewsArticleResponse();

                    mappedNews.NewsArticleId = item.NewsArticleId;
                    mappedNews.NewsTitle = item.NewsTitle;
                    mappedNews.NewsContent = item.NewsContent;
                    mappedNews.CategoryName = item.Category?.CategoryName;
                    mappedNews.CreatedDate = item.CreatedDate;
                    mappedNews.CreatedBy = item.CreatedBy?.AccountName;
                    mappedNews.ModifiedDate = item.ModifiedDate;
                    response.Add(mappedNews);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
