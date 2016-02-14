namespace Steep.Data.Models
{
    using Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment : BaseModel<int>
    {
        [Required]
        public int ChapterId { get; set; }

        public virtual Chapter Chapter { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
