namespace BooksToBoxDemo.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BookModel> Books { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }

    }
}
