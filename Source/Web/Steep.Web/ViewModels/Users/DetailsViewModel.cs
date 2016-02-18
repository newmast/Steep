namespace Steep.Web.ViewModels.Users
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System.Collections.Generic;

    public class DetailsViewModel : IMapFrom<User>
    {
        public string AvatarUrl { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Quote { get; set; }

        public List<Story> Stories { get; set; }

        public List<Chapter> Chapters { get; set; }
    }
}