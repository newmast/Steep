namespace Steep.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Services.Web;

    public class StatisticsController : Controller
    {
        private IStoryService storyService;
        private IGenreService genreService;
        private IChapterService chapterService;
        private IIdentifierProvider identifierProvider;

        public StatisticsController(
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

        public ActionResult Index()
        {
            return this.View();
        }
    }
}