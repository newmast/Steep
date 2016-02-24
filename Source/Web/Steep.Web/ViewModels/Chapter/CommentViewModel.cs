namespace Steep.Web.ViewModels.Chapter
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AvatarUrl { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                 .ForMember(x => x.AuthorName, opts => opts.MapFrom(x => x.User.UserName))
                 .ForMember(x => x.AvatarUrl, opts => opts.MapFrom(x => x.User.AvatarUrl));
        }
    }
}