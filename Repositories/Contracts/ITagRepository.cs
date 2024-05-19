using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ITagRepository
    {
        IEnumerable<TagResponse> GetTags();

        bool AddTag(string newsArticleId, int tagId);

        bool RemoveTag(string newsArticleId, int tagId);
    }
}
