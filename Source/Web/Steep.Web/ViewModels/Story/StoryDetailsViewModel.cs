namespace Steep.Web.ViewModels.Story
{
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;
    using Infrastructure.Mapping;

    public class StoryDetailsViewModel : IMapFrom<Data.Models.Story>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AuthorName { get; set; }

        public ICollection<string> Genres { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Story, StoryDetailsViewModel>()
                .ForMember(x => x.AuthorName, opts => opts.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.Genres, opts => opts.MapFrom(x => x.Genres.Select(y => y.Name)));
        }
    }
}