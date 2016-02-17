namespace Steep.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Microsoft.AspNet.Identity;
    using ViewModels.Story;
    using System.Linq;

    [Authorize]
    public class StoryController : Controller
    {
        private IStoryService storyService;
        private IGenreService genreService;

        public StoryController(IStoryService storyService, IGenreService genreService)
        {
            this.storyService = storyService;
            this.genreService = genreService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new AddStoryViewModel
            {
                SelectedGenres = this.genreService.All().Select(x => x.Name).ToList()
            };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddStoryViewModel model)
        {
            this.storyService.Create(model.Name, this.User.Identity.GetUserId(), model.SelectedGenres);
            return this.RedirectToAction("Index", "Home");
        }
    }
}