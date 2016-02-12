using System.ComponentModel.DataAnnotations;

namespace Steep.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
