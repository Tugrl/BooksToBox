using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly BooksToBoxDbContext booksToBoxDbContext;

        public ProfileRepository(BooksToBoxDbContext booksToBoxDbContext)
        {
            this.booksToBoxDbContext = booksToBoxDbContext;
        }
        public async Task CreateProfileAsync(ProfileModel profile)
        {
            await booksToBoxDbContext.ProfileModels.AddAsync(profile);
            await booksToBoxDbContext.SaveChangesAsync();
        }

        public async Task<ProfileModel> GetProfileAsync(Guid userId)
        {
            return await booksToBoxDbContext.ProfileModels
            .Include(p => p.WantReadBooks)
            .Include(p => p.ReadBooks)
            .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdateProfileAsync(ProfileModel profile)
        {
            booksToBoxDbContext.ProfileModels.Update(profile);
            await booksToBoxDbContext.SaveChangesAsync();
        }
    }
}
