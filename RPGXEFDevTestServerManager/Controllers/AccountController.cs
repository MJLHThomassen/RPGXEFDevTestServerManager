using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RPGXEFDevTestServerManager.Models;

namespace RPGXEFDevTestServerManager.Controllers
{
    [Authorize]
    [RoutePrefix("Account/")]
    public class AccountController : Controller
    {
        public ApplicationUserManager UserManager { get; private set; }
        public ApplicationSignInManager SignInManager { get; private set; }
    
        private IAuthenticationManager AuthenticationManager { get; }

        public AccountController(
            ApplicationUserManager userManager, 
            ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AuthenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpGet]
        [Route("FirstTimeLogin")]
        [AllowAnonymous]
        public async Task<ActionResult> FirstTimeLogin(string userEmail, int userId, string code)
        {
            if (string.IsNullOrEmpty(userEmail) || code == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "FirstTimeLogin" : "Error", new FirstTimeLoginViewModel
            {
                Email = userEmail
            });
        }

        [HttpPost]
        [Route("FirstTimeLogin")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FirstTimeLogin(FirstTimeLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.Users.SingleOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                AddErrors(new IdentityResult("User not found."));
                return View("FirstTimeLogin", model);
            }

            // Set the new password
            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var paswordResetResult = await UserManager.ResetPasswordAsync(user.Id, resetToken, model.Password);

            if (!paswordResetResult.Succeeded)
            {
                AddErrors(paswordResetResult);
                return View(model);
            }

            // Sign user in
            var signInResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            switch (signInResult)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpGet]
        [Route("RegisterFirstUser")]
        [AllowAnonymous]
        public ActionResult RegisterFirstUser()
        {
            // Allow registration only for first user
            if (!UserManager.Users.Any())
            {
                return View("RegisterFirstUser");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("RegisterFirstUser")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterFirstUser(RegisterFirstUserViewModel model)
        {
            // Allow registration only for first user
            if (!UserManager.Users.Any())
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var createResult = await UserManager.CreateAsync(user, model.Password);
                    if (createResult.Succeeded)
                    {
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var confirmEmailResult = await UserManager.ConfirmEmailAsync(user.Id, code);
                        if (confirmEmailResult.Succeeded)
                        {
                            await SignInManager.SignInAsync(user, false, false);

                            return RedirectToAction("Index", "Home");
                        }
                        AddErrors(confirmEmailResult);
                    }
                    AddErrors(createResult);
                }

                // If we got this far, something failed, redisplay form
                return View("RegisterFirstUser", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [Route("ForgotPasswordConfirmation")]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [Route("ResetPasswordConfirmation")]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [Route("LogOff")]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
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