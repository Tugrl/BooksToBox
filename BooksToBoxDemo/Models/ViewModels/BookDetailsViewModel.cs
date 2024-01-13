namespace BooksToBoxDemo.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public Guid BookID { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public string? BookImage { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
        public int TotalLikes { get; set; }
        public bool IsLiked { get; set; }
        public string CommentDescription { get; set; }
        public IEnumerable<BookComment> Comments { get; set; }

    }
}
