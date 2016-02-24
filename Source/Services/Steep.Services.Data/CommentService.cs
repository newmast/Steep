namespace Steep.Services.Data
{
    using System;
    using Contracts;
    using Steep.Data.Common;
    using Steep.Data.Models;

    public class CommentService : ICommentService
    {
        private IDbRepository<Comment> commentRepository;

        public CommentService(IDbRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public void AddComment(string content, string creatorId, int chapterId)
        {
            var newComment = new Comment
            {
                ChapterId = chapterId,
                Content = content,
                UserId = creatorId
            };

            this.commentRepository.Add(newComment);
            this.commentRepository.Save();
        }
    }
}
