namespace Steep.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Steep.Data.Common;
    using Steep.Data.Models;

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

        public void Update(Genre newGenre)
        {
            var dbGenre = this.genreRepository.All().FirstOrDefault(x => x.Id == newGenre.Id);

            dbGenre.IsDeleted = newGenre.IsDeleted;
            dbGenre.ModifiedOn = newGenre.ModifiedOn;
            dbGenre.Name = newGenre.Name;
            dbGenre.Stories = new List<Story>(newGenre.Stories);

            this.genreRepository.Save();
        }
    }
}
