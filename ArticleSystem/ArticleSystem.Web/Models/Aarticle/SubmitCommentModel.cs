using System.ComponentModel.DataAnnotations;
using ArticleSystem.Models;
using ArticleSystem.Web.Infrastructure.Mapping;
using AutoMapper;

namespace ArticleSystem.Web.Models.Aarticle
{
    public class SubmitCommentModel: IMapFrom<Comment>
    {
        [Required]
        public string Comment { get; set; }
       
        public int ArticleId { get; set; }

       
    }
}
