using Microsoft.AspNetCore.Identity;

namespace BooksToBoxDemo.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
