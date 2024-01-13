using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;

namespace BooksToBoxDemo.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> GetAllAsync();
        Task<BookModel> GetAsync(Guid id);
        Task<BookModel> AddAsync(BookModel model);
        Task<BookModel> UpdateAsync(BookModel model);
        Task<BookModel> DeleteAsync(Guid id);
        Task<IEnumerable<HomeViewModel>> SearchAsync(string searchTerm);


    }
}
