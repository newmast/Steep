namespace Steep.Web.ViewModels.Chapter
{
    using System.Collections.Generic;
    using Controllers;
    using Statistics;

    public class ChapterDetailsViewModel
    {
        public int ChapterId { get; set; }

        public AddCommentViewModel AddCommentViewModel { get; set; }

        public StatisticsChapterViewModel StatisticsChapterViewModel { get; set; }

        public ICollection<SingleChapterViewModel> SingleChapterViewModel { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}