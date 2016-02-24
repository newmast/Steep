namespace Steep.Web.ViewModels.Story
{
    using System.Collections.Generic;

    public class StoryChapterViewModel
    {
        public int Id { get; set; }

        //public Data.Models.Chapter Name { get; set; }

        public IEnumerable<StoryChapterViewModel> Chapters { get; set; }
    }
}