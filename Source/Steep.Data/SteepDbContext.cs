namespace Steep.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Steep.Data.Models;

    public class SteepDbContext : IdentityDbContext<User>
    {
        public SteepDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SteepDbContext Create()
        {
            return new SteepDbContext();
        }
    }
}