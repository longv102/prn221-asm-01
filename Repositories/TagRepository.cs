using BO.Dtos;
using DAL;
using Repositories.Contracts;

namespace Repositories
{
    public class TagRepository : ITagRepository
    {
        public bool AddTag(string newsArticleId, int tagId) => TagDAO.Instance.AddTagToNewsArticle(newsArticleId, tagId);

        public IEnumerable<TagResponse> GetTags() => TagDAO.Instance.GetTags();
    }
}
