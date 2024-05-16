using BO.Dtos;
using DAL.Databases;

namespace DAL
{
    public class CategoryDAO
    {
        private CategoryDAO() { }

        private static readonly object _instanceLock = new object();

        private static CategoryDAO? _instance;
        public static CategoryDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CategoryDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var categories = context.Categories.ToList();
                if (!categories.Any())
                    throw new Exception("Category does not exist!");
                var response = new List<CategoryDto>();

                foreach (var category in categories)
                {
                    var mappedCategory = new CategoryDto();
                    mappedCategory.CategoryId = category.CategoryId;
                    mappedCategory.CategoryName = category.CategoryName;
                    mappedCategory.CategoryDesciption = category.CategoryDesciption;

                    response.Add(mappedCategory);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<CategoryDto> GetCategoryByName(string name)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var categories = context.Categories
                    .Where(x => x.CategoryName.Contains(name))
                    .ToList();
                if (!categories.Any())
                    throw new Exception("Category does not exist!");
                var response = new List<CategoryDto>();
                foreach (var item in categories)
                {
                    var mappedCategory = new CategoryDto();

                    mappedCategory.CategoryId = item.CategoryId;
                    mappedCategory.CategoryName = item.CategoryName;
                    mappedCategory.CategoryDesciption = item.CategoryDesciption;
                    response.Add(mappedCategory);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteCategory(short categoryId)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                var category = context.Categories.Find(categoryId);

                if (category is null)
                    throw new Exception("Category does not exist!");
                var checkExistedCategory = context.NewsArticles.Any(x => x.CategoryId == category.CategoryId);
                if (checkExistedCategory)
                    throw new Exception("Cannot delete this category, because it is used in news article!");

                context.Categories.Remove(category);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool CreateCategory(CategoryDto category)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                if (string.IsNullOrEmpty(category.CategoryName))
                    throw new Exception("Category name is required!");
                if (string.IsNullOrEmpty(category.CategoryDesciption))
                    throw new Exception("Category description is required!");

                var mappedCategory = new Category()
                {
                    CategoryName = category.CategoryName,
                    CategoryDesciption = category.CategoryDesciption,
                };
                context.Categories.Add(mappedCategory);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool UpdateCategory(CategoryDto category)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                if (string.IsNullOrEmpty(category.CategoryName))
                    throw new Exception("Category name is required!");
                if (string.IsNullOrEmpty(category.CategoryDesciption))
                    throw new Exception("Category description is required!");
                
                var existedCategory = context.Categories.Find(category.CategoryId);
                if (existedCategory is null)
                    throw new Exception("Category does not exist!");
                // Update
                existedCategory.CategoryName = category.CategoryName;
                existedCategory.CategoryDesciption = category.CategoryDesciption;
                context.Categories.Update(existedCategory);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }
        
    }
}
