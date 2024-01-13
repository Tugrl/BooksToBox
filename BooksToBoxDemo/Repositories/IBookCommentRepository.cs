using BooksToBoxDemo.Models;

namespace BooksToBoxDemo.Repositories
{
    public interface IBookCommentRepository
    {
        Task<CommentModel> AddAsync(CommentModel comment);
        Task<IEnumerable<CommentModel>> GetCommentsByBookIdAsync(Guid bookId);
    }
}
