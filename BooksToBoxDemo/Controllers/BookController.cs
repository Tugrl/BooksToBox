using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using BooksToBoxDemo.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksToBoxDemo.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookLikeRepository bookLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBookCommentRepository bookCommentRepository;
        private readonly IProfileBookRepository profileBookRepository;
        private readonly IProfileRepository profileRepository;

        public BookController(IBookRepository bookRepository, IBookLikeRepository bookLikeRepository,SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager, IBookCommentRepository bookCommentRepository, IProfileBookRepository profileBookRepository, IProfileRepository profileRepository)
        {
            this.bookRepository = bookRepository;
            this.bookLikeRepository = bookLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.bookCommentRepository = bookCommentRepository;
            this.profileBookRepository = profileBookRepository;
            this.profileRepository = profileRepository;
        }
        [HttpGet("/Home/Book")]
        public async Task<IActionResult> Index(Guid bookId)
        {
            var isLiked = false;
            var book = await bookRepository.GetAsync(bookId);
            var bookDetailsViewModel = new BookDetailsViewModel();


            if (book!=null)
            {              
                var totalLikes =await bookLikeRepository.GetTotalLikes(book.BookID);
                if (signInManager.IsSignedIn(User))
                {
                    var likesForBook = await bookLikeRepository.GetLikesForBook(book.BookID);
                    var userId = userManager.GetUserId(User);
                    if (userId!=null)
                    {
                        var likeFromUser = likesForBook.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        isLiked=likeFromUser!=null;
                    }
                }
                var bookComments=await bookCommentRepository.GetCommentsByBookIdAsync(book.BookID);
                var bookCommentsForView = new List<BookComment>();
                foreach (var comment in bookComments)
                {
                    bookCommentsForView.Add(new BookComment
                    {
                        Description = comment.Description,
                        DateAdded = comment.DateAdded,
                        Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    });
                }
                bookDetailsViewModel = new BookDetailsViewModel
                {
                    BookID = book.BookID,
                    BookName = book.BookName,
                    BookImage = book.BookImage,
                    Author = book.Author,
                    Categories = book.Categories,
                    TotalLikes = totalLikes,
                    IsLiked = isLiked,
                    Comments=bookCommentsForView
                };
            }
            return View(bookDetailsViewModel);
        }
        [HttpPost("/Home/Book")]
        public async Task<IActionResult> Index(BookDetailsViewModel bookDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var commentModel = new CommentModel
                {
                    BookId = bookDetailsViewModel.BookID,
                    Description = bookDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded= DateTime.Now
                };

                await bookCommentRepository.AddAsync(commentModel);

                return RedirectToAction("Index", "Book",new {bookId=bookDetailsViewModel.BookID});
            }
            return Forbid();           
        }
        [HttpGet]
        public async Task<IActionResult> AddReadList()
        {
            var userProfile = await profileRepository.GetProfileAsync(Guid.Parse(userManager.GetUserId(User)));
            return View(userProfile);
        }
        [HttpPost]
        public async Task<IActionResult> AddReadList(Guid bookId)
        {
            var book = await bookRepository.GetAsync(bookId);

            if (book == null)
            {
                // Kitap bulunamazsa hata durumu veya başka bir işlem yapılabilir
                return NotFound();
            }

            // Kitabı ProfileBookView'a çevir
            var profileBookView = new ProfileBookView
            {
                UserID = Guid.Parse(userManager.GetUserId(User)),
                BookID = bookId,
                BookName = book.BookName,
                Author = book.Author,
                BookImage = book.BookImage,
                Categories = book.Categories
            };

            // ProfileModel'i al veya oluştur
            var userProfile = await profileRepository.GetProfileAsync(Guid.Parse(userManager.GetUserId(User)));

            if (userProfile == null)
            {
                userProfile = new ProfileModel
                {
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    WantReadBooks = new List<ProfileBookView> { profileBookView }
                };
                await profileRepository.CreateProfileAsync(userProfile); // Profil oluşturuluyor
            }
            else
            {
                // Profil daha önce oluşturulmuşsa, mevcut kitaplar koleksiyonuna yeni kitap ekleniyor
                userProfile.WantReadBooks.Add(profileBookView);
                await profileRepository.UpdateProfileAsync(userProfile); // Profil güncelleniyor
            }

            return View(userProfile);
        }
        [HttpGet]
        public async Task<IActionResult> AddIveReadList()
        {
            var userProfile = await profileRepository.GetProfileAsync(Guid.Parse(userManager.GetUserId(User)));
            return View(userProfile);
        }
        [HttpPost]
        public async Task<IActionResult> AddIveReadList(Guid bookId)
        {
            var book = await bookRepository.GetAsync(bookId);

            if (book == null)
            {
                // Kitap bulunamazsa hata durumu veya başka bir işlem yapılabilir
                return NotFound();
            }

            // Kitabı ProfileBookView'a çevir
            var profileBookView = new ProfileBookView
            {
                UserID = Guid.Parse(userManager.GetUserId(User)),
                BookID = bookId,
                BookName = book.BookName,
                Author = book.Author,
                BookImage = book.BookImage,
                Categories = book.Categories
            };

            // ProfileModel'i al veya oluştur
            var userProfile = await profileRepository.GetProfileAsync(Guid.Parse(userManager.GetUserId(User)));

            if (userProfile == null)
            {
                userProfile = new ProfileModel
                {
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    ReadBooks = new List<ProfileBookView> { profileBookView }
                };
                await profileRepository.CreateProfileAsync(userProfile); // Profil oluşturuluyor
            }
            else
            {
                // Profil daha önce oluşturulmuşsa, mevcut kitaplar koleksiyonuna yeni kitap ekleniyor
                userProfile.ReadBooks.Add(profileBookView);
                await profileRepository.UpdateProfileAsync(userProfile); // Profil güncelleniyor
            }

            return View(userProfile);
        }
    }
}
