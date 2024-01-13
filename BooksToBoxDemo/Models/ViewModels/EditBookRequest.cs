namespace BooksToBoxDemo.Models.ViewModels
{
    public class EditBookRequest
    {
        public Guid BookID { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }


    }
}
