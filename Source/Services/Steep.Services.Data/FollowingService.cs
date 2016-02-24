namespace Steep.Services.Data
{
    using System;
    using Contracts;
    using Steep.Data.Models;
    using Steep.Data.Common;
    using System.Linq;
    public class FollowingService : IFollowingService
    {
        private IDbRepository<Following> followerRepository;

        public FollowingService(IDbRepository<Following> followerRepository)
        {
            this.followerRepository = followerRepository;
        }

        public void Follow(User follower, string followedId)
        {
            var following = this.followerRepository.All().FirstOrDefault(x => x.FollowedUserId == followedId);
            if (following == null)
            {
                following = new Following();
                following.FollowedUserId = followedId;
                this.followerRepository.Add(following);
                this.followerRepository.Save();
            }

            following.Followers.Add(follower);
            this.followerRepository.Add(following);
            this.followerRepository.Save();
        }

        //public void Unfollow(string followerId, string followedId)
        //{
        //    var followerConnection = this.followerRepository.All()
        //        .FirstOrDefault(x => (x.UserFollowerId == followerId) && (x.FollowedUserId == followedId));

        //    this.followerRepository.Delete(followerConnection);
        //    this.followerRepository.Save();
        //}
    }
}
