namespace Steep.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class ReadChapter : BaseModel<int>
    {
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int ChapterId { get; set; }

        public virtual Chapter Chapter { get; set; }
    }
}
