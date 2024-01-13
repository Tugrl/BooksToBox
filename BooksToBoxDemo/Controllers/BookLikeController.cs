using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using BooksToBoxDemo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksToBoxDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLikeController : ControllerBase
    {
        private readonly IBookLikeRepository bookLikeRepository;

        public BookLikeController(IBookLikeRepository bookLikeRepository)
        {
            this.bookLikeRepository = bookLikeRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model = new BookLikeModel
            {
                BookId = addLikeRequest.BookId,
                UserId = addLikeRequest.UserId,
            };
            await bookLikeRepository.AddLikeForBook(model);
            return Ok();
        }
        [HttpGet]
        [Route("{bookId:guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBook([FromRoute] Guid bookId)
        {
            var totalLikes = await bookLikeRepository.GetTotalLikes(bookId);
            return Ok(totalLikes);
        }
    }
}
