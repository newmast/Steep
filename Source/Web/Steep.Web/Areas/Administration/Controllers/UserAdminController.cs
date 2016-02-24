namespace Steep.Web.Areas.Administration.Controllers
{
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;
    using System.Web;
    using System.Web.Mvc;

    public class UserAdminController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var userManager = this.HttpContext.GetOwinContext().GetUserManager<SteepUserManager>();
            DataSourceResult result = userManager.Users
                .To<UserAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, UserAdminViewModel user)
        {
            var userManager = this.HttpContext.GetOwinContext().GetUserManager<SteepUserManager>();
            var model = userManager.FindById(user.Id);
            if (this.ModelState.IsValid)
            {
                model.Firstname = user.Firstname;
                model.Lastname = user.Lastname;
                model.Quote = user.Quote;
                model.UserName = user.UserName;
                model.Email = user.Email;

                userManager.Update(model);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, UserAdminViewModel user)
        {
            var userManager = this.HttpContext.GetOwinContext().GetUserManager<SteepUserManager>();
            var model = userManager.FindById(user.Id);

            if (this.ModelState.IsValid)
            {
                userManager.Delete(model);
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }
    }
}