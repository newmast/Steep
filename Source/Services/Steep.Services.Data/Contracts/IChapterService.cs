namespace Steep.Services.Data.Contracts
{
    using System.Linq;
    using Steep.Data.Models;

    public interface IChapterService
    {
        IQueryable<Chapter> All();

        IQueryable<Chapter> GetLatestTen();

        Chapter Add(Chapter chapterToAdd);
    }
}
