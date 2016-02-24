namespace Steep.Services.Data.Contracts
{
    public interface IStatisticsService
    {
        int GetNumberOfChapters();

        int GetNumberOfStories();

        int GetNumberOfGenres();

        int GetNumberOfChaptersForStory(int storyId);

        int GetChapterViews(int chapterId);

        int GetStoryViews(int storyId);
    }
}
