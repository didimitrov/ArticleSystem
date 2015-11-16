using System.Collections.Generic;
using ArticleSystem.Models;
using ArticleSystem.Web.Infrastructure.Mapping;
using AutoMapper;

namespace ArticleSystem.Web.Models.Aarticle
{
    public class ArticleDetailsViewModel: IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsAvalible { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public bool UserCanVote { get; set; }

        public int Votes { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(x => x.Votes, opt => opt.MapFrom(c => c.Votes.Count))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Url))
                .ForMember(x=>x.Comments, opt=>opt.MapFrom(x=>x.Comments));
        }
    }
}