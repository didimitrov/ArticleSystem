using System.ComponentModel.DataAnnotations;
using ArticleSystem.Models;
using ArticleSystem.Web.Infrastructure.Mapping;
using AutoMapper;

namespace ArticleSystem.Web.Models.Home
{
    public class ArticleHomeViewModel: IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        
        public string Url { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleHomeViewModel>()
            .ForMember(p => p.Name, options => options.MapFrom(p => p.Name));
        }
    }
}