namespace Steep.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity.Owin;
    using ViewModels.Users;
    using Services.Data.Contracts;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class UsersController : BaseController
    {
        private IStoryService storyService;
        private IChapterService chapterService;

        public UsersController(IStoryService storyService, IChapterService chapterService)
        {
            this.storyService = storyService;
            this.chapterService = chapterService;
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var userManager = this.Request.GetOwinContext().GetUserManager<SteepUserManager>();
            var user = await userManager.FindByNameAsync(id);

            var model = new DetailsViewModel
            {
                AvatarUrl = user.AvatarUrl,
                Chapters = this.GetUserChapters(user.Id),
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Quote = user.Quote,
                Stories = this.GetUserStories(user.Id)
            };

            return this.View(model);
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