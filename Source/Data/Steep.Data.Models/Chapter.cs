namespace Steep.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Chapter
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int? PreviousChapterId { get; set; }

        public virtual Chapter PreviousChapter { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MaxLength(15000)]
        public string Content { get; set; }

        [Required]
        public int StoryId { get; set; }

        public virtual Story Story { get; set; }
    }
}
