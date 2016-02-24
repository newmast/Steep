namespace Steep.Web.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int ChapterId { get; set; }
    }
}