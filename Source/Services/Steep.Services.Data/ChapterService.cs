namespace Steep.Services.Data
{
    using System.Linq;
    using Contracts;
    using Steep.Data.Models;
    using Steep.Data.Common;
    using System;

    public class ChapterService : IChapterService
    {
        private IDbRepository<Chapter> chapterRepository;

        public ChapterService(IDbRepository<Chapter> chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }

        public Chapter Add(Chapter chapterToAdd)
        {
            this.chapterRepository.Add(chapterToAdd);
            this.chapterRepository.Save();

            return chapterToAdd;
        }

        public IQueryable<Chapter> All()
        {
            return this.chapterRepository.All();
        }

        public IQueryable<Chapter> GetChaptersByStoryId(int storyId)
        {
            return this.chapterRepository.All()
                       .Where(x => x.StoryId == storyId)
                       .OrderBy(x => x.CreatedOn);
        }

        public IQueryable<Chapter> GetLastestItems(int numberOfItems)
        {
            return this.chapterRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(10);
        }

        public bool IsTitleUnique(string title)
        {
            return this.chapterRepository
                .All()
                .FirstOrDefault(x => x.Title == title) == null;
        }
    }
}
