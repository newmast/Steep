namespace Steep.Web.ViewModels.Home
{
    using AutoMapper;
    using Infrastructure.Mapping;

    public class IndexStoryViewModel : IMapFrom<Data.Models.Story>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string StoryIdentifier { get; set; }

        public string Name { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Story, IndexStoryViewModel>()
                .ForMember(x => x.AuthorName, opts => opts.MapFrom(x => x.Author.UserName));
        }
    }
}