using System.Collections.Generic;

namespace ArticleSystem.Web.Models.Aarticle
{
    public class ArticleDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }


        public string Description { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public bool UserCanVote { get; set; }

        public int Votes { get; set; }
    }
}