using System;
using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
