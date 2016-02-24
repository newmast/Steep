namespace Steep.Services.Data
{
    using System.Linq;
    using Contracts;
    using Steep.Data.Common;
    using Steep.Data.Models;

    public class StatisticsService : IStatisticsService
    {
        private IDbRepository<Story> storyRepository;
        private IDbRepository<Chapter> chapterRepository;
        private IDbRepository<Genre> genreRepository;

        public StatisticsService(
            IDbRepository<Story> storyRepository,
            IDbRepository<Chapter> chapterRepository,
            IDbRepository<Genre> genreRepository)
        {
            this.storyRepository = storyRepository;
            this.chapterRepository = chapterRepository;
            this.genreRepository = genreRepository;
        }

        public int GetNumberOfChapters()
        {
            return this.chapterRepository.All().Count();
        }

        public int GetNumberOfGenres()
        {
            return this.genreRepository.All().Count();
        }

        public int GetNumberOfStories()
        {
            return this.storyRepository.All().Count();
        }
    }
}
