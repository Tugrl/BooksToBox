using BooksToBoxDemo.Data;
using BooksToBoxDemo.Models;
using BooksToBoxDemo.Models.ViewModels;
using BooksToBoxDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksToBoxDemo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoryController : Controller
    {
        
        private readonly ICategoryRepository categoryRepository;
        
        public AdminCategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;

        }
        
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryAddRequest categoryAddRequest)
        {
            
            if (ModelState.IsValid==false)
            {
                return View();
            }
            //Mapping CategoryAddRequest to Category model 
            var category = new CategoryModel
            {
                CategoryName = categoryAddRequest.CategoryName

            };
            
            await categoryRepository.AddAsync(category);
            return RedirectToAction("CategoryAdd");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var categories = await categoryRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid categoryId)
        {
            
            var category =await categoryRepository.GetAsync(categoryId);
            if (category!=null)
            {
                var editCategoryRequest = new CategoryEditRequest
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                };
                return View(editCategoryRequest);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditRequest categoryEditRequest)
        {
            var category = new CategoryModel
            {
                CategoryName = categoryEditRequest.CategoryName
            };
            await categoryRepository.UpdateAsync(category);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryEditRequest categoryEditRequest)
        {
            await categoryRepository.DeleteAsync(categoryEditRequest.CategoryID);
            return RedirectToAction();
        }


    }
}
