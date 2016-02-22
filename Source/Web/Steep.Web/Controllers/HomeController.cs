namespace Steep.Web.Controllers
{
    using Services.Data.Contracts;
    using Services.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        const int MaxSymbolsPerChapterContentPreview = 600;
        private IChapterService chapterService;
        private IIdentifierProvider identifierProvider;

        public HomeController(IChapterService chapterService, IIdentifierProvider identifierProvider)
        {
            this.chapterService = chapterService;
            this.identifierProvider = identifierProvider;
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                NotificationMessage = this.TempData["Notification"] == null ? null : this.TempData["Notification"].ToString(),
                LatestChapters = this.GetLatestChapters()
            };

            return this.View(model);
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
                    Author = x.Author.Firstname + " " + x.Author.Lastname,
                    AlreadyRead = this.UserId == null ? false : (x.UsersThatRead.FirstOrDefault(y => y.Id == this.UserId) != null)
                })
                .ToList();

            foreach (var item in items)
            {
                item.Identifier = this.identifierProvider.EncodeId(item.Id.ToString());
            }

            return items;

        }
    }
}
