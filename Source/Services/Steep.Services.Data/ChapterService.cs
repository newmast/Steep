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
            try
            {
                this.chapterRepository.Save();
            }
            catch(Exception e)
            {

            }

            return chapterToAdd;
        }

        public IQueryable<Chapter> All()
        {
            return this.chapterRepository.All();
        }

        public IQueryable<Chapter> GetLastestItems(int numberOfItems)
        {
            return this.chapterRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(10);
        }
    }
}
