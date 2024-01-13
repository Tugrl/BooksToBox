using BooksToBoxDemo.Models;

namespace BooksToBoxDemo.Repositories
{
    public interface IProfileRepository
    {
        Task<ProfileModel> GetProfileAsync(Guid userId);
        Task CreateProfileAsync(ProfileModel profile);
        Task UpdateProfileAsync(ProfileModel profile);
    }
}
