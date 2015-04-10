using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Models
{
    public sealed class Article
    {
       public Article()
        {
            Comments=new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Url { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Vote> Votes { get; set; }


    }
}
