namespace Steep.Web.ViewModels.Home
{
    using AutoMapper;
    using Infrastructure.Mapping;

    public class IndexChapterViewModel : IMapFrom<Data.Models.Chapter>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public bool AlreadyRead { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Chapter, IndexChapterViewModel>()
              .ForMember(x => x.AuthorName, opts => opts.MapFrom(x => x.Author.Firstname + " " + x.Author.Lastname));
        }
    }
}