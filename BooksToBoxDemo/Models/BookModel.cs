using System.ComponentModel.DataAnnotations;

namespace BooksToBoxDemo.Models
{
    public class BookModel
    {
        [Key]
        public Guid BookID { get; set; }
        public string? BookName {  get; set; }
        public string? Author { get; set; }
        public string? BookImage { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }       
        public ICollection<BookLikeModel> Likes { get; set; }
        public ICollection<CommentModel> Comments { get; set; }

    }
}
