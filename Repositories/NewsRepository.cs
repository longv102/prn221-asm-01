using BO.Dtos;
using DAL;
using Repositories.Contracts;

namespace Repositories
{
    public class NewsRepository : INewsRepository
    {
        public bool CreateNews(NewsArticleRequest request)
            => NewsArticleDAO.Instance.CreateNews(request);

        public bool DeleteNews(string id) => NewsArticleDAO.Instance.DeleteNews(id);

        public IEnumerable<NewsArticleResponse> GetNews()
            => NewsArticleDAO.Instance.GetNews();

        public NewsArticleResponse GetNewsById(string id)
            => NewsArticleDAO.Instance.GetNewsById(id);

        public IEnumerable<NewsArticleResponse> GetNewsByStaffId(short id)
            => NewsArticleDAO.Instance.GetNewsByStaffId(id);

        public IEnumerable<NewsArticleResponse> SearchNewsByContent(string content)
            => NewsArticleDAO.Instance.GetNewsByContent(content);

        public IEnumerable<NewsArticleResponse> SearchNewsByTitle(string title)
            => NewsArticleDAO.Instance.GetNewsByTitle(title);

        public bool UpdateNews(NewsArticleRequest request)
            => NewsArticleDAO.Instance.UpdateNews(request);

        public IEnumerable<TagResponse> ViewTagByNewsArticleId(string id)
            => NewsArticleDAO.Instance.GetTagByNewsId(id);
    }
}
