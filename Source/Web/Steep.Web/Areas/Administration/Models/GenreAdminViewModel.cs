namespace Steep.Web.Areas.Administration.Models
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class GenreAdminViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}