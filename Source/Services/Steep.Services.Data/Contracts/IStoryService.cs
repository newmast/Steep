namespace Steep.Services.Data.Contracts
{
    using System.Collections.Generic;
    using Steep.Data.Models;

    public interface IStoryService
    {
        Story Create(string storyName, string creatorId, IEnumerable<string> genreNames);
    }
}
