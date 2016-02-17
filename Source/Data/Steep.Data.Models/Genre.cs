namespace Steep.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    public class Genre : BaseModel<int>
    {
        public Genre()
        {
            this.Stories = new HashSet<Story>();
        }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }
}
