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
            throw new NotImplementedException();
        }

        public IQueryable<Chapter> GetLastestItems(int numberOfItems)
        {
            return this.chapterRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(10);
        }
    }
}
