namespace Steep.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public string NotificationMessage { get; set; }

        public List<IndexChapterViewModel> LatestChapters { get; set; }
    }
}