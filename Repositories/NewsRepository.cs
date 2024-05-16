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

        public IEnumerable<NewsArticleResponse> GetNewsByStaffId(short id)
            => NewsArticleDAO.Instance.GetNewsByStaffId(id);

        public bool UpdateNews(NewsArticleRequest request)
            => NewsArticleDAO.Instance.UpdateNews(request);

        public IEnumerable<TagResponse> ViewTagByNewsArticleId(string id)
            => NewsArticleDAO.Instance.GetTagByNewsId(id);
    }
}
