using BO.Dtos;

namespace Repositories.Contracts
{
    public interface INewsRepository
    {
        IEnumerable<NewsArticleResponse> GetNewsByStaffId(short id);

        IEnumerable<NewsArticleResponse> GetNews();

        IEnumerable<TagResponse> ViewTagByNewsArticleId(string id);

        NewsArticleResponse GetNewsById(string id);
        
        bool DeleteNews(string id);

        bool CreateNews(NewsArticleRequest request);

        bool UpdateNews(NewsArticleRequest request);

        IEnumerable<NewsArticleResponse> SearchNewsByTitle(string title);

        IEnumerable<NewsArticleResponse> SearchNewsByContent(string content);

    }
}
