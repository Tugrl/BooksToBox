using BooksToBoxDemo.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BooksToBoxDemo.Models
{
    public class ProfileModel
    {
        [Key]
        public Guid UserId { get; set; }
        public List<ProfileBookView>? WantReadBooks { get; set; }
        public List<ProfileBookView>? ReadBooks { get; set; }
    }
}
