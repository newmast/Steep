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
    public class StoryAdminController : BaseController
    {
        private IStoryService storyService;

        public StoryAdminController(IStoryService storyService)
        {
            this.storyService = storyService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Stories_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.storyService
                .All()
                .To<StoryAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Stories_Update([DataSourceRequest]DataSourceRequest request, StoryAdminViewModel story)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Story
                {
                    Id = story.Id,
                    Name = story.Name,
                    ModifiedOn = DateTime.Now
                };

                this.storyService.Update(entity);
            }

            return this.Json(new[] { story }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Stories_Destroy([DataSourceRequest]DataSourceRequest request, StoryAdminViewModel story)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Story
                {
                    Id = story.Id,
                    IsDeleted = true
                };

                this.storyService.Update(entity);
            }

            return this.Json(new[] { story }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
