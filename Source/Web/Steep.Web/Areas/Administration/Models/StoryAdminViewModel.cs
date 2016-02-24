namespace Steep.Web.Areas.Administration.Models
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class StoryAdminViewModel : IMapFrom<Story>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Story, StoryAdminViewModel>()
                 .ForMember(x => x.AuthorName, opts => opts.MapFrom(x => x.Author.UserName));
        }
    }
}