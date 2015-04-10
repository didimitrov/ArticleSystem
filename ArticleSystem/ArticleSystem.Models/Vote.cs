using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public virtual Article Article { get; set; }
        public int ArticleId { get; set; }

        public virtual ApplicationUser VotedBy { get; set; }
        public string VotedById { get; set; }
    }
}
