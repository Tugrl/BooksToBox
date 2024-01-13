using BooksToBoxDemo.Models;

namespace BooksToBoxDemo.Repositories
{
    public interface IBookLikeRepository
    {
        Task<int>GetTotalLikes(Guid bookId);
        Task<IEnumerable<BookLikeModel>> GetLikesForBook(Guid bookId);
        Task<BookLikeModel> AddLikeForBook(BookLikeModel bookLike);
    }
}
