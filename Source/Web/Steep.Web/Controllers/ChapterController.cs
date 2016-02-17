namespace Steep.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels.Chapter;

    public class ChapterController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            var extenedStory = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "First story", Value = "0" },
                new SelectListItem() { Text = "Second story", Value = "1" }
            };
            var previousChapter = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "First previousChapter", Value = "0" },
                new SelectListItem() { Text = "Second previousChapter", Value = "1" }
            };
            var model = new AddChapterViewModel
            {
                ExtendedStory = extenedStory,
                PreviousChapterSelect = previousChapter
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddChapterViewModel model)
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}