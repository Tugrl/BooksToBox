using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BooksToBoxDbContext booksToBoxDbContext;
        public CategoryRepository(BooksToBoxDbContext booksToBoxDbContext) { 
        this.booksToBoxDbContext = booksToBoxDbContext;
        
        } 
        public async Task<CategoryModel> AddAsync(CategoryModel category)
        {
            await booksToBoxDbContext.Categories.AddAsync(category);
            await booksToBoxDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryModel?> DeleteAsync(Guid Id)
        {
            var deletedCategory = await booksToBoxDbContext.Categories.FindAsync(Id);
            if (deletedCategory != null)
            {
                booksToBoxDbContext.Categories.Remove(deletedCategory);
                await booksToBoxDbContext.SaveChangesAsync();
                return deletedCategory;            
            }
            return null;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await booksToBoxDbContext.Categories.ToListAsync();
        }

        public Task<CategoryModel?> GetAsync(Guid Id)
        {
           return booksToBoxDbContext.Categories.FirstOrDefaultAsync(x=>x.CategoryID == Id);
        }

        public async Task<CategoryModel?> UpdateAsync(CategoryModel category)
        {
            var existingCategory = await booksToBoxDbContext.Categories.FindAsync(category.CategoryID);
            if (existingCategory!=null)
            {
                existingCategory.CategoryName = category.CategoryName;
                await booksToBoxDbContext.SaveChangesAsync(); 
                return existingCategory;
            }
            return null;
        }
    }
}
