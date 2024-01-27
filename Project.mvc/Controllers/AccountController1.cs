using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Project.mvc.Models;
namespace mvcRegistrations

{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };


            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
               
                var role = await _userManager.AddToRoleAsync(user, "User");

                if (role.Succeeded)
                {
                   
                    return RedirectToAction("Login");

                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            var login = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (login != null && login.Succeeded)
            {
                // Set a success message in TempData
                
                TempData["SuccessMessage"] = "Login succeeded";

                Thread.Sleep(5000);
                // Redirect to the "Index" action in the "home" controller
                return RedirectToAction("Index", "home");
                
            }
            else
            {
                // Set an error message in TempData
                TempData["ErrorMessage"] = "Login failed";

                // Return the login view with the model to repopulate the form fields
                return View(model);
            }
        }



        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("Index", "home");
        }


    }

}


