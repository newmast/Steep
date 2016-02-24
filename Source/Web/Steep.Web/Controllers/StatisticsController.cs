namespace Steep.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Statistics;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;
    using System.Linq;
    public class StatisticsController : BaseController
    {
        private IStatisticsService stats;

        public StatisticsController(IStatisticsService stats)
        {
            this.stats = stats;
        }

        public StatisticsIndexViewModel GetStats()
        {
            return new StatisticsIndexViewModel
            {
                NumberOfChapters = this.stats.GetNumberOfChapters(),
                NumberOfStories = this.stats.GetNumberOfStories(),
                NumberOfGenres = this.stats.GetNumberOfGenres(),
                NumberOfUsers = this.GetNumberOfUsers()
            };
        }

        public ActionResult Index()
        {
            var model = this.Cache.Get("stats", () => this.GetStats(), 60 * 15);
            return this.View(model);
        }

        private int GetNumberOfUsers()
        {
            var userManager = this.HttpContext.GetOwinContext().GetUserManager<SteepUserManager>();

            return userManager.Users.Count();
        }
    }
}