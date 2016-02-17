namespace Steep.Web.ViewModels.Chapter
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AddChapterViewModel
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("Chapter title:")]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(15000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Write here:")]
        public string Content { get; set; }

        public int ExtendedStoryId { get; set; }

        [DisplayName("What story does this extend?")]
        public IEnumerable<SelectListItem> ExtendedStory { get; set; }
        
        public int PreviousChapterSelectId { get; set; }

        [DisplayName("What was the previous chapter?")]
        public IEnumerable<SelectListItem> PreviousChapterSelect { get; set; }
    }
}