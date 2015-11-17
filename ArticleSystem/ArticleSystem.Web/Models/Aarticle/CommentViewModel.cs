using System;
using ArticleSystem.Models;
using ArticleSystem.Web.Infrastructure.Mapping;
using AutoMapper;

namespace ArticleSystem.Web.Models.Aarticle
{
    public class CommentViewModel: IMapFrom<Comment>,IHaveCustomMappings
    {

        // public CommentViewModel()
        //{
        //}

        //public CommentViewModel(int articleId)
        //{
        //    this.ArticleId = articleId;
        //}


        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string AuthorUsername { get; set; }

        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.AuthorUsername, options => options.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content));
        }
    }
}
