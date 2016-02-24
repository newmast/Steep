namespace Steep.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;
    public class DetailsViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string AvatarUrl { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Quote { get; set; }

        public ICollection<Story> Stories { get; set; }

        public ICollection<Chapter> Chapters { get; set; }
    }
}