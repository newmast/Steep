namespace Steep.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using System.Collections.Generic;

    public class Story : BaseModel<int>
    {
        public Story()
        {
            this.Genres = new HashSet<Genre>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}
