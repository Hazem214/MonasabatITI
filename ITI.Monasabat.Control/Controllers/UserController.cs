using ITI.Monasabat.Control.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monasapat.Models;

namespace ITI.Monasabat.Control.Controllers
{
    public class UserController : Controller
    {
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        public UserController(UserManager<User> _usermaneger, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = _usermaneger;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
                return View();
            else
            {
                User User = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                IdentityResult result =
                await UserManager.CreateAsync(User, model.Password);

              

                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(
                        i =>
                        {
                            ModelState.AddModelError("", i.Description);
                        }
                        );
                    return View();
                }
                else
                {
                    await UserManager.AddToRoleAsync(User, "User");

                    return RedirectToAction("SignIn", "User");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View();
            else
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "invalid User Name or Password ");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "home");
                }
                
            }
            return View();
        }

        public IActionResult SignOUt()
        {
            SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
