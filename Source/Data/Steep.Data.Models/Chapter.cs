﻿namespace Steep.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Chapter : BaseModel<int>
    {
        public Chapter()
        {
            this.UsersThatRead = new HashSet<User>();
            this.PreviousChapters = new HashSet<Chapter>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public virtual ICollection<Chapter> PreviousChapters { get; set; }

        public int? PreviousChapterId { get; set; }

        public virtual Chapter PreviousChapter { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<User> UsersThatRead { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int NumberOfViews { get; set; }

        [Required]
        [MaxLength(35000)]
        public string Content { get; set; }

        [Required]
        public int StoryId { get; set; }

        public virtual Story Story { get; set; }
    }
}
