namespace Steep.Services.Data
{
    using System.Linq;
    using Steep.Data.Models;
    using Contracts;
    using Steep.Data.Common;

    public class GenreService : IGenreService
    {
        private IDbRepository<Genre> genreRepository;

        public GenreService(IDbRepository<Genre> genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public IQueryable<Genre> All()
        {
            return this.genreRepository.All();
        }
    }
}
