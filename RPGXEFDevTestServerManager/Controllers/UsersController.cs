using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RPGXEFDevTestServerManager.Models;

namespace RPGXEFDevTestServerManager.Controllers
{
    [RoutePrefix("Users/")]
    public class UsersController : Controller
    {
        public ApplicationSignInManager SignInManager { get; private set; }
        public ApplicationUserManager UserManager { get; private set; }

        public UsersController(
            ApplicationSignInManager signInManager,
            ApplicationUserManager userManager)
        {

            SignInManager = signInManager;
            UserManager = userManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new UsersViewModel
            {
                Users = UserManager.Users.ToList()
            };

            return View(model);
        }


        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                // Register user with new random password
                var result = await UserManager.CreateAsync(user, "TempPassword" + Guid.NewGuid());

                if (result.Succeeded)
                {
                    // Send an email with this link
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    var homeUrl = Url.Action("Index", "Home", new {}, Request.Url.Scheme);
                    var callbackUrl = Url.Action("FirstTimeLogin", "Account", new {userEmail = user.Email, userId = user.Id, code = code }, Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id, 
                        "Confirm your RPG-X EF Dev Test Server account", 
                        $"An account to use the RPG-X EF Dev Test Server on {homeUrl} was created for you by {User.Identity.Name}.\n\n" +
                        $"Please confirm your account by clicking the following link: {callbackUrl}");

                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [Route("Delete")]
        public ActionResult Delete(int userId)
        {
            return View(new DeleteViewModel
            {
                UserId = userId,
                UserName = UserManager.FindById(userId).UserName
            });
        }

        [HttpPost]
        [Route("DeleteYes")]
        [MultipleSubmitButtons(Name = "Delete", Argument = "Yes")]
        public ActionResult DeleteYes(DeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager.Delete(UserManager.FindById(model.UserId));

                return RedirectToAction("Index");
            }

            return View("Delete", model);
        }

        [HttpPost]
        [Route("DeleteNo")]
        [MultipleSubmitButtons(Name = "Delete", Argument = "No")]
        public ActionResult DeleteNo(DeleteViewModel model)
        {
            return RedirectToAction("Index");
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UserManager != null)
                {
                    UserManager.Dispose();
                    UserManager = null;
                }

                if (SignInManager != null)
                {
                    SignInManager.Dispose();
                    SignInManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}