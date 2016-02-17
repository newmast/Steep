namespace Steep.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Chapter;

    [Authorize]
    public class ChapterController : BaseController
    {
        private IStoryService storyService;
        private IChapterService chapterService;

        public ChapterController(IStoryService storyService, IChapterService chapterService)
        {
            this.storyService = storyService;
            this.chapterService = chapterService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            this.Cache.Get("Stories", () => this.GetStoriesForExtension(), 10 * 60);
            var model = new AddChapterViewModel
            {
                ExtendedStory = this.Cache.Get("Stories", () => this.GetStoriesForExtension(), 10 * 60),
                PreviousChapterSelect = this.GetPreviousAvailableChapters()
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddChapterViewModel model)
        {
            var isTitleUnique = this.chapterService.IsTitleUnique(model.Title);
            if (!this.ModelState.IsValid ||
                !isTitleUnique)
            {
                // TODO: Cache these
                model.PreviousChapterSelect = this.GetPreviousAvailableChapters();
                model.ExtendedStory = this.GetStoriesForExtension();
                model.Content = model.Content ?? string.Empty;

                if (!isTitleUnique)
                {
                    this.ModelState.AddModelError(string.Empty, "Title already in use!");
                }

                return this.View(model);
            }

            int? previousChapterId = null;
            if (model.PreviousChapterSelectId != -1)
            {
                previousChapterId = model.PreviousChapterSelectId;
            }

            var chapterToAdd = new Chapter
            {
                Content = model.Content,
                PreviousChapterId = previousChapterId,
                StoryId = model.ExtendedStoryId,
                AuthorId = this.UserId,
                Title = model.Title
            };

            this.chapterService.Add(chapterToAdd);
            this.TempData["Notification"] = "Congratulations! Chapter successfully added!";
            return this.RedirectToAction("Index", "Home");
        }

        public List<SelectListItem> GetStoriesForExtension()
        {
            return this.storyService
                .All()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        public List<SelectListItem> GetPreviousAvailableChapters()
        {
            var chapterList = new List<SelectListItem>();
            chapterList.Add(new SelectListItem
                            {
                                Text = "Introduction Chapter",
                                Value = "-1"
                            });

            var serviceChapters = this.chapterService
                .All()
                .Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                })
                .ToList();

            chapterList.AddRange(serviceChapters);

            return chapterList;
        }
    }
}