using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksToBoxDemo.Models
{
    public class BookLikeModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

        
    }
}
