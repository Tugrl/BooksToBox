using System.ComponentModel.DataAnnotations;

namespace BooksToBoxDemo.Models.ViewModels
{
    public class ProfileBookView
    {
        [Key]
        public Guid ReadBookId { get; set; }
        public Guid UserID { get; set; }
        public Guid BookID { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public string? BookImage { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
}
