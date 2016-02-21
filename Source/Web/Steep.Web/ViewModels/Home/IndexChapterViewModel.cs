namespace Steep.Web.ViewModels.Home
{
    public class IndexChapterViewModel
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public bool AlreadyRead { get; set; }
    }
}