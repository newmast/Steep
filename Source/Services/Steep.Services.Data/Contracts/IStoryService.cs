namespace Steep.Services.Data.Contracts
{
    using System.Collections.Generic;
    using Steep.Data.Models;
    using System.Linq;
    public interface IStoryService
    {
        Story Create(string storyName, string creatorId, IEnumerable<string> genreNames);

        IQueryable<Story> All();

        IQueryable<Story> GetById(int id);

        IQueryable<Story> AllForUser(string userId);
    }
}
