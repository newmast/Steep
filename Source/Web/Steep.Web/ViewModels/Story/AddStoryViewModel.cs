namespace Steep.Web.ViewModels.Story
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AddStoryViewModel
    {
        [DisplayName("What name?")]
        public string Name { get; set; }

        [DisplayName("What genre?")]
        public IEnumerable<string> SelectedGenres { get; set; }

    }
}