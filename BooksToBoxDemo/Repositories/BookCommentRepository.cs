using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Repositories
{
    public class BookCommentRepository : IBookCommentRepository
    {
        private readonly BooksToBoxDbContext booksToBoxDbContext;

        public BookCommentRepository(BooksToBoxDbContext booksToBoxDbContext)
        {
            this.booksToBoxDbContext = booksToBoxDbContext;
        }
        public async Task<CommentModel> AddAsync(CommentModel comment)
        {
            await booksToBoxDbContext.Comments.AddAsync(comment);
            await booksToBoxDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByBookIdAsync(Guid bookId)
        {
            return await booksToBoxDbContext.Comments.Where(x => x.BookId == bookId).ToListAsync();
        }
    }
}
