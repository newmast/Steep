namespace Steep.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Story : BaseModel<int>
    {
        public Story()
        {
            this.Genres = new HashSet<Genre>();
            this.Chapters = new HashSet<Chapter>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public int NumberOfViews { get; set; }
    }
}
