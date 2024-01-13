using BooksToBoxDemo.Models.ViewModels;
using BooksToBoxDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksToBoxDemo.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users=await userRepository.GetAll();
            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var user in users)
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    UserId = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    EmailAdress = user.Email,
                });
            }
            return View(usersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = userViewModel.Username,
                Email = userViewModel.Email,
            };
            var identityResult=await userManager.CreateAsync(identityUser, userViewModel.Password);
            if (identityResult!=null)
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };
                    if (userViewModel.AdminRoleCheckBox)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult != null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }
            return View();
        }
        [HttpPost("/AdminUsers/Delete/{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);
                if (identityResult!=null&&identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }
    }
}
