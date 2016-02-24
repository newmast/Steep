namespace Steep.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Contracts;
    using Services.Web;
    using ViewModels.Users;

    public class UsersController : BaseController
    {
        private IStoryService storyService;
        private IChapterService chapterService;
        private IIdentifierProvider identifierProvider;

        public UsersController(
            IStoryService storyService,
            IChapterService chapterService,
            IIdentifierProvider identifierProvider)
        {
            this.storyService = storyService;
            this.chapterService = chapterService;
            this.identifierProvider = identifierProvider;
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var decodedId = this.identifierProvider.DecodeId(id);
            var userManager = this.Request.GetOwinContext().GetUserManager<SteepUserManager>();
            var user = userManager.Users.FirstOrDefault(x => x.Id == decodedId);

            var model = new DetailsViewModel
            {
                Id = id,
                AvatarUrl = user.AvatarUrl,
                Chapters = this.GetUserChapters(user.Id),
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Quote = user.Quote,
                Stories = this.GetUserStories(user.Id)
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult GetFollowed(string id)
        {
            var userManager = this.Request.GetOwinContext().GetUserManager<SteepUserManager>();
            var user = userManager.Users.FirstOrDefault(x => x.Id == this.UserId);

            return this.Json(new
            {
                success = true,
                message = "Followed!"
            });
        }

        private List<Story> GetUserStories(string id)
        {
            return this.storyService.All()
                .Where(x => x.AuthorId == id)
                .ToList();
        }

        private List<Chapter> GetUserChapters(string id)
        {
            return this.chapterService.All()
                .Where(x => x.AuthorId == id)
                .ToList();
        }
    }
}