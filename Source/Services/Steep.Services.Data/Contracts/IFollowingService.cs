namespace Steep.Services.Data.Contracts
{
    using Steep.Data.Models;

    public interface IFollowingService
    {
        void Follow(User follower, string followedId);

        //void Unfollow(string followerId, string followedId);
    }
}
