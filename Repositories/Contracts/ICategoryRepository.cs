using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetCategories();

        IEnumerable<CategoryDto> SearchCategoryByName(string name);

        bool CreateCategory(CategoryDto category);

        bool UpdateCategory(CategoryDto category);

        bool DeleteCategory(short id);
    }
}
