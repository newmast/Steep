namespace Steep.Services.Data.Contracts
{
    public interface ICommentService
    {
        void AddComment(string content, string creatorId, int chapterId);
    }
}
