namespace Steep.Services.Data.Contracts
{
    using System.Linq;
    using Steep.Data.Models;

    public interface IChapterService
    {
        IQueryable<Chapter> All();

        IQueryable<Chapter> GetLastestItems(int numberOfItems);

        bool IsTitleUnique(string title);

        IQueryable<Chapter> GetChaptersByStoryId(int storyId);

        Chapter Add(Chapter chapterToAdd);

        IQueryable<Chapter> GetById(int id);

        void Update(Chapter entity);
    }
}
