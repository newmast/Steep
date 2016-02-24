namespace Steep.Data.Models
{
    using System.Collections.Generic;
    using Common.Models;

    public class Following : BaseModel<int>
    {
        public Following()
        {
            this.Followers = new HashSet<User>();
        }

        public string FollowedUserId { get; set; }

        public virtual User FollowedUser { get; set; }

        public virtual ICollection<User> Followers { get; set; }
    }
}
