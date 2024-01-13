using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using BooksToBoxDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksToBoxDemo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;

        private readonly BooksToBoxDbContext booksToBoxDbContext;
        public AdminBookController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BookAdd()
        {
            var categories = await categoryRepository.GetAllAsync();
            var model = new BookAddRequests
            {
                Categories = categories.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> BookAdd(BookAddRequests bookAddRequests)
        {
                       
            var book = new BookModel
            {
                BookName = bookAddRequests.BookName,
                Author = bookAddRequests.Author,
                BookImage = bookAddRequests.BookImage,
            };
            var selectedCategories = new List<CategoryModel>();
            foreach (var selectedCategoryId in bookAddRequests.SelectedCategories) 
            {
                var selectedCategoryIdAsGuid = Guid.Parse(selectedCategoryId);
                var existingCategory = await categoryRepository.GetAsync(selectedCategoryIdAsGuid);

                if (existingCategory != null)
                {
                    selectedCategories.Add(existingCategory);
                }
                book.Categories = selectedCategories;
            }

            await bookRepository.AddAsync(book);
            return RedirectToAction("BookList");
        }

        [HttpGet]
        public async Task<IActionResult> BookList()
        {
            var bookList = await bookRepository.GetAllAsync();
            return View(bookList);
        }
        [HttpGet("AdminBook/BookEdit/{bookId}")]
        public async Task<IActionResult> BookEdit(Guid bookId)
        {
            var book =await bookRepository.GetAsync(bookId);
            var categories = await categoryRepository.GetAllAsync();
            if (book!=null)
            {
                var model = new BookEditRequest
                {
                    BookID = book.BookID,
                    BookName=book.BookName,
                    Author = book.Author,
                    BookImage = book.BookImage,
                    Categories = categories.Select(x => new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryID.ToString()
                    }),
                    SelectedCategories = book.Categories.Select(x => x.CategoryID.ToString()).ToArray(),

                };
                return View(model);
            }
            return View(null);
           
        }
        [HttpPost]
        public async Task<IActionResult> BookEdit(BookEditRequest bookEditRequest)
        {
            var bookModel = new BookModel
            {
                BookID = bookEditRequest.BookID,
                BookName=bookEditRequest.BookName,
                BookImage=bookEditRequest.BookImage,
                Author = bookEditRequest.Author

            };
            var selectedCategories = new List<CategoryModel>();
            foreach (var selectedCategory in bookEditRequest.SelectedCategories)
            {
                if (Guid.TryParse(selectedCategory, out var category))
                {
                    var foundCategory = await categoryRepository.GetAsync(category);
                    if (foundCategory != null)
                    {
                        selectedCategories.Add(foundCategory);
                    }

                }
            }
            bookModel.Categories = selectedCategories;
            var updatedBook = await bookRepository.UpdateAsync(bookModel);
            if (updatedBook != null)
            {
                return RedirectToAction("BookEdit");
            }
            return RedirectToAction("BookList");
        }
        [HttpPost]
        public async Task<IActionResult> BookDelete(BookEditRequest bookEditRequest)
        {
            var deletedBook = await bookRepository.DeleteAsync(bookEditRequest.BookID);
            if (deletedBook != null)
            {
                return RedirectToAction("BookList");
            }
            return RedirectToAction("BookEdit", new {id=bookEditRequest.BookID});
        }
    }
}
