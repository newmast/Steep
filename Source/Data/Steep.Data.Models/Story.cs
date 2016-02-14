namespace Steep.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Story : BaseModel<int>
    {
        [Required]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
