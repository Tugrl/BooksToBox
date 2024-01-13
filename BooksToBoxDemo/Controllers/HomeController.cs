using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using BooksToBoxDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace BooksToBoxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly BooksToBoxDbContext booksToBoxDbContext;

        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository, ICategoryRepository categoryRepository,BooksToBoxDbContext booksToBoxDbContext)
        {
            _logger = logger;
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
            this.booksToBoxDbContext = booksToBoxDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var bookRepo=await bookRepository.GetAllAsync();
            var categoryRepo = await categoryRepository.GetAllAsync();
            var model = new HomeViewModel
            {
                Books = bookRepo,
                Categories = categoryRepo,
            };
            return View(model);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            var viewModel = new HomeViewModel();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var matchingBooks = await booksToBoxDbContext.Books
                    .Include(b=>b.Categories)
                    .Where(b => b.BookName.Contains(searchTerm))
                    .ToListAsync();

                viewModel.Books = matchingBooks;
            }

            return View("Index", viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
