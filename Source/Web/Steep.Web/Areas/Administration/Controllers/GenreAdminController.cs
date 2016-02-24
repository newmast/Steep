namespace Steep.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using Web.Controllers;
    using Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Data.Models;
    using System;
    public class GenreAdminController : BaseController
    {
        private IGenreService genreService;

        public GenreAdminController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Genres_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.genreService
                .All()
                .To<GenreAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genres_Update([DataSourceRequest]DataSourceRequest request, GenreAdminViewModel genre)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Genre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    ModifiedOn = DateTime.Now,
                };

                this.genreService.Update(entity);
            }

            return this.Json(new[] { genre }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genres_Destroy([DataSourceRequest]DataSourceRequest request, GenreAdminViewModel genre)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Genre
                {
                    Id = genre.Id,
                    IsDeleted = true
                };

                this.genreService.Update(entity);
            }

            return this.Json(new[] { genre }.ToDataSourceResult(request, this.ModelState));
        }
    }
}