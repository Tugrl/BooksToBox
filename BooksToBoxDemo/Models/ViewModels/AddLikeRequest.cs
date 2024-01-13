namespace BooksToBoxDemo.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
    }
}
