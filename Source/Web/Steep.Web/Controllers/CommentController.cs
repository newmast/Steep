namespace Steep.Web.Controllers
{
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels.Chapter;
    [Authorize]
    public class CommentController : BaseController
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Add(AddCommentViewModel model)
        {
            this.commentService.AddComment(
                model.Content,
                model.AuthorId,
                model.ChapterId);
            return this.Content("Comment added!");
        }
    }
}