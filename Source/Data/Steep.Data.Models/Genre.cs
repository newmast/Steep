namespace Steep.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Genre : BaseModel<int>
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
