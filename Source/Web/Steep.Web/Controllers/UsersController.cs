namespace Steep.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity.Owin;
    using ViewModels.Users;

    public class UsersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var userManager = this.Request.GetOwinContext().GetUserManager<SteepUserManager>();
            var user = await userManager.FindByNameAsync(id);

            var model = (DetailsViewModel)this.Mapper.Map(user, typeof(User), typeof(DetailsViewModel));
            model.Users = userManager.Users;

            return this.View(model);
        }
    }
}