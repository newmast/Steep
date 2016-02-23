namespace Steep.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Microsoft.AspNet.Identity;
    using ViewModels.Story;
    using System.Linq;

    [Authorize]
    public class StoryController : BaseController
    {
        private IStoryService storyService;
        private IGenreService genreService;

        public StoryController(IStoryService storyService, IGenreService genreService)
        {
            this.storyService = storyService;
            this.genreService = genreService;
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var dbId = this.identifierProvider.DecodeId(id);
            var chapterDetails = this.chapterService
                .GetById(dbId)
                .To<ChapterDetailsViewModel>()
                .FirstOrDefault();
            chapterDetails.StoryUrl = this.identifierProvider.EncodeId(chapterDetails.StoryId.ToString());
            return this.View(chapterDetails);
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
            this.storyService.Create(model.Name, this.UserId, model.SelectedGenres);
            return this.RedirectToAction("Index", "Home");
        }
    }
}