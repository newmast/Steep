namespace Steep.Web.ViewModels.Chapter
{
    using Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using System.Collections.Generic;
    public class SingleChapterViewModel : IMapFrom<Data.Models.Chapter>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        public int? PreviousChapterId { get; set; }

        public string Author { get; set; }

        [MaxLength(15000)]
        public string Content { get; set; }

        public string StoryUrl { get; set; }

        public int StoryId { get; set; }

        public string StoryName { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Chapter, SingleChapterViewModel>()
                .ForMember(x => x.Author, opts => opts.MapFrom(m => m.Author.UserName))
                .ForMember(x => x.StoryName, opts => opts.MapFrom(m => m.Story.Name));
        }
    }
}