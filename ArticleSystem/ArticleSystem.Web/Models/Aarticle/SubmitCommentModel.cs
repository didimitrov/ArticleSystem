using System.ComponentModel.DataAnnotations;
using ArticleSystem.Models;

namespace ArticleSystem.Web.Models.Aarticle
{
    public class SubmitCommentModel
    {
        [Required]
        //[ShouldNotContainEmail]
        public string Comment { get; set; }

       
        [Required]
        public int ArticleId { get; set; }
    }
}
