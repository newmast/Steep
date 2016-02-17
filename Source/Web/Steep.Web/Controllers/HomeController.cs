namespace Steep.Web.Controllers
{
    using System.Web.Mvc;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel
            {

            }
            return this.View(model);
        }
    }
}
