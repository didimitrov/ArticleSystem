using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
