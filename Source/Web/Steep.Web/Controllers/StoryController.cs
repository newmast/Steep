namespace Steep.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Services.Web;
    using Infrastructure.Mapping;
    using ViewModels.Story;
    using System.Collections.Generic;
    using Data.Models;
    [Authorize]
    public class StoryController : BaseController
    {
        private IStoryService storyService;
        private IGenreService genreService;
        private IChapterService chapterService;
        private IIdentifierProvider identifierProvider;

        public StoryController(
            IStoryService storyService,
            IGenreService genreService,
            IIdentifierProvider identifierProvider,
            IChapterService chapterService)
        {
            this.storyService = storyService;
            this.genreService = genreService;
            this.chapterService = chapterService;
            this.identifierProvider = identifierProvider;
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var dbId = int.Parse(this.identifierProvider.DecodeId(id));
            var storyDetails = this.storyService
                .GetById(dbId)
                .To<StoryDetailsViewModel>()
                .FirstOrDefault();
            
            return this.View(storyDetails);
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

        public ActionResult GetChapters(int id)
        {
            var chapters = this.chapterService
                .GetChaptersByStoryId(id)
                .Select(x => new
                {
                    Title = x.Title,
                    Id = x.Id
                })
                .ToList();
            return this.Json(chapters, JsonRequestBehavior.AllowGet);
        }
    }
}