using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;

namespace BooksToBoxDemo.Repositories
{
    public interface IProfileBookRepository
    {
        Task<IEnumerable<ProfileBookView>> GetReadListAsync();
        Task<IEnumerable<ProfileBookView>> GetIveReadListAsync();
        Task<ProfileBookView> AddReadListAsync(ProfileBookView model);
        Task<ProfileBookView> AddIveReadListAsync(ProfileBookView model);
        Task<ProfileModel> AddReadListProfileModelAsync(ProfileModel model);
        Task<ProfileModel> AddIveReadListProfileModelAsync(ProfileModel model);
    }
}
