﻿namespace Steep.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using ViewModels.Manage;

    [Authorize]
    public class ManageController : BaseController
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private ApplicationSignInManager signInManager;

        private SteepUserManager userManager;

        public ManageController()
        {
        }

        public ManageController(SteepUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public SteepUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<SteepUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager => this.HttpContext.GetOwinContext().Authentication;

        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess
                                             ? "Your password has been changed."
                                             : message == ManageMessageId.SetPasswordSuccess
                                                   ? "Your password has been set."
                                                   : message == ManageMessageId.SetTwoFactorSuccess
                                                         ? "Your two-factor authentication provider has been set."
                                                         : message == ManageMessageId.Error
                                                               ? "An error has occurred."
                                                               : message == ManageMessageId.AddPhoneSuccess
                                                                     ? "Your phone number was added."
                                                                     : message == ManageMessageId.RemovePhoneSuccess
                                                                           ? "Your phone number was removed."
                                                                           : string.Empty;
            return this.View();
        }

        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result =
                await
                this.UserManager.RemoveLoginAsync(
                    this.UserId,
                    new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.UserId);
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }

            return this.RedirectToAction("ManageLogins", new { Message = message });
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result =
                await
                this.UserManager.ChangePasswordAsync(
                    this.UserId,
                    model.OldPassword,
                    model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.UserId);
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
        }

        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return this.View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.AddPasswordAsync(this.UserId, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.UserId);
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return this.RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(
                provider,
                this.Url.Action("LinkLoginCallback", "Manage"),
                this.User.Identity.GetUserId());
        }

        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo =
                await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, this.UserId);
            if (loginInfo == null)
            {
                return this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }

            var result = await this.UserManager.AddLoginAsync(this.UserId, loginInfo.Login);
            return result.Succeeded
                       ? this.RedirectToAction("ManageLogins")
                       : this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(this.UserId);
            return user?.PasswordHash != null;
        }

        private bool HasPhoneNumber()
        {
            var user = this.UserManager.FindById(this.UserId);
            return user?.PhoneNumber != null;
        }
    }
}
