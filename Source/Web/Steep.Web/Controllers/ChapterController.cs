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
            var previousChapterSelect = this.GetPreviousAvailableChapters();
            previousChapterSelect.Add(new SelectListItem()
            {
                Text = "None (this is the first chapter)",
                Value = "-1"
            });
            var model = new AddChapterViewModel
            {
                ExtendedStory = this.GetStoriesForExtension(),
                PreviousChapterSelect = previousChapterSelect
            };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddChapterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
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
            this.TempData["Notification"] = "Congratulations, chapter successfully added!";
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
            return this.chapterService
                .All()
                .Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                })
                .ToList();
        }
    }
}