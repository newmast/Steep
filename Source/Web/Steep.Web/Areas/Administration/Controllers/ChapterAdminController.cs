namespace Steep.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Services.Data.Contracts;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ChapterAdminController : BaseController
    {
        private IChapterService chapterService;

        public ChapterAdminController(IChapterService chapterService)
        {
            this.chapterService = chapterService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Chapters_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.chapterService
                .All()
                .To<ChapterAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Chapters_Update([DataSourceRequest]DataSourceRequest request, ChapterAdminViewModel chapter)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Chapter
                {
                    Id = chapter.Id,
                    Title = chapter.Title,
                    ModifiedOn = DateTime.Now,
                    Content = chapter.Content,
                    StoryId = chapter.StoryId,
                    PreviousChapterId = chapter.PreviousChapterId,
                    AuthorId = chapter.AuthorId,
                };

                this.chapterService.Update(entity);
            }

            return this.Json(new[] { chapter }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Chapters_Destroy([DataSourceRequest]DataSourceRequest request, ChapterAdminViewModel chapter)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Chapter
                {
                    Id = chapter.Id,
                    IsDeleted = true
                };

                this.chapterService.Update(entity);
            }

            return this.Json(new[] { chapter }.ToDataSourceResult(request, this.ModelState));
        }
    }
}