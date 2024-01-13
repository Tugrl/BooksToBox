using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksToBoxDbContext booksToBoxDbContext;

        public BookRepository(BooksToBoxDbContext booksToBoxDbContext)
        {
            this.booksToBoxDbContext = booksToBoxDbContext;
        }
        public async Task<BookModel> AddAsync(BookModel model)
        {
           await booksToBoxDbContext.Books.AddAsync(model);
            await booksToBoxDbContext.SaveChangesAsync();
            return model;
        }

        public async Task<BookModel?> DeleteAsync(Guid id)
        {
            var existingBook = await booksToBoxDbContext.Books.FindAsync(id);
            if (existingBook != null)
            {
                booksToBoxDbContext.Books.Remove(existingBook);
                await booksToBoxDbContext.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }

        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            return await booksToBoxDbContext.Books.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BookModel?> GetAsync(Guid id)
        {
            return await booksToBoxDbContext.Books.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.BookID == id);
        }
        public async Task<IEnumerable<HomeViewModel>> SearchAsync(string searchedTerm)
        {
           var searchedBooks = await booksToBoxDbContext.Books.Include(x=>x.Categories).FirstOrDefaultAsync(x=>x.BookName.Contains(searchedTerm));
           return (IEnumerable<HomeViewModel>)searchedBooks;
        }

        public async Task<BookModel> UpdateAsync(BookModel model)
        {
            var existingBook = await booksToBoxDbContext.Books.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.BookID == model.BookID);
            if (existingBook != null)
            {
                existingBook.BookID = model.BookID;
                existingBook.BookName = model.BookName;
                existingBook.BookImage= model.BookImage;
                existingBook.Author = model.Author;
                existingBook.Categories = model.Categories;
                await booksToBoxDbContext.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }


    }
}
