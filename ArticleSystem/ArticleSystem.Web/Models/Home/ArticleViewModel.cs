using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Web.Models.Home
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        
        public string Url { get; set; }
    }
}