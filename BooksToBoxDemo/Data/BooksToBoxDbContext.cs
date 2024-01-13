using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Data
{
    public class BooksToBoxDbContext : DbContext
    {
        public BooksToBoxDbContext(DbContextOptions<BooksToBoxDbContext> options) : base(options)
        {
        }   
        public DbSet<BookModel> Books { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<BookLikeModel> BookLike { get; set; }
        public DbSet<ProfileBookView> ProfileBooks { get; set; }
        public DbSet<ProfileModel> ProfileModels { get; set; }
    }
}
