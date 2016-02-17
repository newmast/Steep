namespace Steep.Services.Data.Contracts
{
    using System.Linq;
    using Steep.Data.Models;

    public interface IChapterService
    {
        IQueryable<Chapter> All();

        IQueryable<Chapter> GetLastestItems(int numberOfItems);

        IQueryable<Chapter> GetChaptersByStoryId(int storyId);

        Chapter Add(Chapter chapterToAdd);
    }
}
