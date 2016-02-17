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
        
        [Required]
        [MaxLength(15000)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int ExtendedStoryId { get; set; }

        public IEnumerable<SelectListItem> ExtendedStory { get; set; }

        public int PreviousChapterSelectId { get; set; }

        public IEnumerable<SelectListItem> PreviousChapterSelect { get; set; }
    }
}