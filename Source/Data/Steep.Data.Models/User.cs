namespace Steep.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    public class User : IdentityUser
    {
        public User()
        {
            this.ReadChapters = new HashSet<Chapter>();
            this.Stories = new HashSet<Story>();
        }

        [MaxLength(25)]
        public string Firstname { get; set; }

        [MaxLength(40)]
        public string Lastname { get; set; }

        [MaxLength(150)]
        public string AvatarUrl { get; set; }

        [MaxLength(150)]
        public string Quote { get; set; }

        public virtual ICollection<Story> Stories { get; set; }

        public virtual ICollection<Chapter> ReadChapters { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
