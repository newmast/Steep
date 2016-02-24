namespace Steep.Web.Areas.Administration.Models
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ChapterAdminViewModel : IMapFrom<Chapter>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? PreviousChapterId { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public int StoryId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Chapter, ChapterAdminViewModel>()
                 .ForMember(x => x.AuthorName, opts => opts.MapFrom(x => x.Author.UserName));
        }
    }
}