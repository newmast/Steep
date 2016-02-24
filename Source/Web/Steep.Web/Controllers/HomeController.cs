namespace Steep.Web.Controllers
{
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using Services.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int MaxSymbolsPerChapterContentPreview = 600;
        private IChapterService chapterService;
        private IStoryService storyService;
        private IIdentifierProvider identifierProvider;

        public HomeController(
            IChapterService chapterService,
            IIdentifierProvider identifierProvider,
            IStoryService storyService)
        {
            this.chapterService = chapterService;
            this.identifierProvider = identifierProvider;
            this.storyService = storyService;
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                NotificationMessage = this.TempData["Notification"] == null ? null : this.TempData["Notification"].ToString(),
                LatestChapters = this.GetLatestChapters(),
                LatestStories = this.GetLatestStories()
            };

            return this.View(model);
        }

        private List<IndexStoryViewModel> GetLatestStories()
        {
            var items = this.storyService.GetLastestStories(10)
                .To<IndexStoryViewModel>()
                .ToList();

            foreach (var item in items)
            {
                item.StoryIdentifier = this.identifierProvider.EncodeId(item.Id.ToString());
                item.AuthorId = this.identifierProvider.EncodeId(item.AuthorId);
            }

            return items;
        }

        private List<IndexChapterViewModel> GetLatestChapters()
        {
            var items = this.chapterService
                .GetLastestItems(10)
                .Select(x => new IndexChapterViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content.Length <= MaxSymbolsPerChapterContentPreview ? x.Content : x.Content.Substring(0, MaxSymbolsPerChapterContentPreview - 2) + "...",
                    AuthorName = x.Author.Firstname + " " + x.Author.Lastname,
                    AuthorId = x.AuthorId,
                    AlreadyRead = this.UserId == null ? false : (x.UsersThatRead.FirstOrDefault(y => y.Id == this.UserId) != null)
                })
                .ToList();

            foreach (var item in items)
            {
                item.Identifier = this.identifierProvider.EncodeId(item.Id.ToString());
                item.AuthorId = this.identifierProvider.EncodeId(item.AuthorId);
            }

            return items;

        }
    }
}
