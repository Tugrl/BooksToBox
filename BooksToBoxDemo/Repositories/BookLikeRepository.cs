
using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Repositories
{
    public class BookLikeRepository : IBookLikeRepository
    {
        private readonly BooksToBoxDbContext booksToBoxDbContext;

        public BookLikeRepository(BooksToBoxDbContext booksToBoxDbContext)
        {
            this.booksToBoxDbContext = booksToBoxDbContext;
        }

        public async Task<BookLikeModel> AddLikeForBook(BookLikeModel bookLike)
        {
            await booksToBoxDbContext.BookLike.AddAsync(bookLike);
            await booksToBoxDbContext.SaveChangesAsync();
            return bookLike;
        }

        public async Task<IEnumerable<BookLikeModel>> GetLikesForBook(Guid bookId)
        {
            return await booksToBoxDbContext.BookLike.Where(x=>x.BookId == bookId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid bookId)
        {
            return await booksToBoxDbContext.BookLike.CountAsync(x=>x.BookId==bookId);
        }
    }
}
