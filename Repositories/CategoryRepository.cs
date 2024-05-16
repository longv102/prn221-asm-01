using BO.Dtos;
using DAL;
using Repositories.Contracts;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public bool CreateCategory(CategoryDto category)
            => CategoryDAO.Instance.CreateCategory(category);

        public bool DeleteCategory(short id)
            => CategoryDAO.Instance.DeleteCategory(id);

        public IEnumerable<CategoryDto> GetCategories() => CategoryDAO.Instance.GetCategories();

        public IEnumerable<CategoryDto> SearchCategoryByName(string name)
            => CategoryDAO.Instance.GetCategoryByName(name);

        public bool UpdateCategory(CategoryDto category)
            => CategoryDAO.Instance.UpdateCategory(category);
    }
}
