namespace Steep.Web.Areas.Administration.Models
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserAdminViewModel : IMapFrom<User>
    {
        private string avatarUrl;

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        //public string AvatarUrl
        //{
        //    get
        //    {
        //        if (this.avatarUrl == null)
        //        {
        //            this.avatarUrl = "~/Content/Images/default-avatar.png";
        //        }

        //        return this.avatarUrl;
        //    }

        //    set
        //    {
        //        this.avatarUrl = value;
        //    }
        //}

        public string Quote { get; set; }
    }
}