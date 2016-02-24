namespace Steep.Services.Data.Contracts
{
    public interface IStatisticsService
    {
        int GetNumberOfChapters();

        int GetNumberOfStories();

        int GetNumberOfGenres();
    }
}
