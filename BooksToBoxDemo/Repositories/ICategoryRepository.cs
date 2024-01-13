using BooksToBoxDemo.Models;

namespace BooksToBoxDemo.Repositories
{
    public interface ICategoryRepository
    {
       Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel?> GetAsync(Guid Id);
        Task<CategoryModel> AddAsync(CategoryModel model);
        Task<CategoryModel?> UpdateAsync(CategoryModel model);   
        Task<CategoryModel?> DeleteAsync(Guid Id);
    }
}
