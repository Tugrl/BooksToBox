using System.ComponentModel.DataAnnotations;

namespace BooksToBoxDemo.Models
{
    public class CategoryModel
    {
        [Key]
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<BookModel> Books { get; set; }

    }
}
