namespace Steep.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Story
    {
        public int Id { get; set; }

        [Required]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
