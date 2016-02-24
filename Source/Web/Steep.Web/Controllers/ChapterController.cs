namespace Steep.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using Services.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Chapter;
    using ViewModels.Home;
    using ViewModels.Statistics;
    [Authorize]
    public class ChapterController : BaseController
    {
        private IStoryService storyService;
        private IChapterService chapterService;
        private IStatisticsService statisticsService;
        private IIdentifierProvider identifierProvider;

        public ChapterController(
            IStoryService storyService,
            IChapterService chapterService,
            IIdentifierProvider identifierProvider,
            IStatisticsService statisticsService)
        {
            this.storyService = storyService;
            this.chapterService = chapterService;
            this.identifierProvider = identifierProvider;
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public ActionResult All()
        {
            var model = this.chapterService.All()
                .To<IndexChapterViewModel>()
                .ToList();

            foreach (var item in model)
            {
                item.Identifier = this.identifierProvider.EncodeId(item.Id.ToString());
            }

            return this.View(model);
        }

        public ActionResult Chapters_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.storyService
                .All()
                .To<IndexChapterViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new AddChapterViewModel
            {
                ExtendedStory = this.GetStoriesForExtension(),
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

        [HttpGet]
        public ActionResult Details(string id)
        {
            var dbId = int.Parse(this.identifierProvider.DecodeId(id));

            var chapterDetails = new ChapterDetailsViewModel();
            chapterDetails.ChapterId = dbId;
            var chaptersList = new List<SingleChapterViewModel>();
            chapterDetails.Comments = new List<CommentViewModel>();
            var comments = this.chapterService
                .GetById(dbId)
                .Select(x => x.Comments)
                .FirstOrDefault();

            foreach (var comment in comments)
            {
                var mappedComment = this.Mapper.Map<CommentViewModel>(comment);
                chapterDetails.Comments.Add(mappedComment);
            }

            this.GetChildren(ref chaptersList, dbId);

            chapterDetails.SingleChapterViewModel = chaptersList;

            foreach (var chapter in chapterDetails.SingleChapterViewModel)
            {
                chapter.StoryUrl = this.identifierProvider.EncodeId(chapter.StoryId.ToString());
            }

            chapterDetails.StatisticsChapterViewModel = this.Cache.Get("GetChapterStats", () => this.GetStats(dbId), 60 * 15);
            this.chapterService.IncreaseViewCount(dbId);

            return this.View(chapterDetails);
        }

        public void GetChildren(ref List<SingleChapterViewModel> chapters, int? id)
        {
            if (id == null)
            {
                return;
            }

            var chapter = this.chapterService
                .All()
                .Where(x => x.Id == id)
                .To<SingleChapterViewModel>()
                .FirstOrDefault();
            chapters.Add(chapter);

            if (chapter.PreviousChapterId != null)
            {
                this.GetChildren(ref chapters, chapter.PreviousChapterId);
            }
        }

        private StatisticsChapterViewModel GetStats(int id)
        {
            return new StatisticsChapterViewModel
            {
                ChapterViews = this.statisticsService.GetChapterViews(id)
            };
        }

        private List<SelectListItem> GetStoriesForExtension()
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

        private List<SelectListItem> GetPreviousAvailableChapters()
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