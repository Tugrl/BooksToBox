using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Repositories
{
    public class ProfileBookRepository : IProfileBookRepository
    {
        private readonly BooksToBoxDbContext booksToBoxDbContext;

        public ProfileBookRepository(BooksToBoxDbContext booksToBoxDbContext)
        {
            this.booksToBoxDbContext = booksToBoxDbContext;
        }
        public async Task<ProfileBookView> AddIveReadListAsync(ProfileBookView model)
        {
            await booksToBoxDbContext.ProfileBooks.AddAsync(model);
            await booksToBoxDbContext.SaveChangesAsync();
            return model;
        }

        public async Task<ProfileModel> AddIveReadListProfileModelAsync(ProfileModel model)
        {
            await booksToBoxDbContext.ProfileModels.AddAsync(model);
            await booksToBoxDbContext.SaveChangesAsync();
            return model;
        }

        public async Task<ProfileBookView> AddReadListAsync(ProfileBookView model)
        {
            await booksToBoxDbContext.ProfileBooks.AddAsync(model);
            await booksToBoxDbContext.SaveChangesAsync();
            return model;
        }

        public async Task<ProfileModel> AddReadListProfileModelAsync(ProfileModel model)
        {
            await booksToBoxDbContext.ProfileModels.AddAsync(model);
            await booksToBoxDbContext.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<ProfileBookView>> GetIveReadListAsync()
        {
            return await booksToBoxDbContext.ProfileBooks.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<IEnumerable<ProfileBookView>> GetReadListAsync()
        {
            return await booksToBoxDbContext.ProfileBooks.Include(x => x.Categories).ToListAsync();
        }
    }
}
