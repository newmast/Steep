namespace Steep.Services.Data.Contracts
{
    using Steep.Data.Models;
    using System.Linq;

    public interface IGenreService
    {
        IQueryable<Genre> All();
    }
}
