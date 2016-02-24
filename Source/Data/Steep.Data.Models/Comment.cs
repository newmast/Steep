namespace Steep.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Comment : BaseModel<int>
    {
        [Required]
        public int ChapterId { get; set; }

        public virtual Chapter Chapter { get; set; }
        
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
