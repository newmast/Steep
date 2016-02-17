namespace Steep.Web.ViewModels.Chapter
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AddChapterViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public string PreviousChapterId { get; set; }

        public IEnumerable<SelectListItem> PreviousChapterSelect { get; set; }

        [Required]
        [MaxLength(15000)]
        public string Content { get; set; }

        public string ExtendedStoryId { get; set; }

        public IEnumerable<SelectListItem> ExtendedStory { get; set; }
    }
}