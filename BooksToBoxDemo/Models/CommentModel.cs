using System.ComponentModel.DataAnnotations;

namespace BooksToBoxDemo.Models
{
    public class CommentModel
    {

        [Key] 
        public Guid Id { get; set; }
        public string Description{ get; set; }
        public Guid BookId {  get; set; }
        public Guid UserId {  get; set; }
        public DateTime DateAdded { get; set; }




    }
}
