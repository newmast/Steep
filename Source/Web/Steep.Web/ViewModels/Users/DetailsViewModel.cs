namespace Steep.Web.ViewModels.Users
{
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class DetailsViewModel : IMapFrom<User>
    {
        public string AvatarUrl { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Quote { get; set; }

        public IQueryable<User> Users { get; set; }
    }
}