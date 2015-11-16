using ArticleSystem.Models;
using ArticleSystem.Web.Infrastructure.Mapping;

namespace ArticleSystem.Web.Models.Home
{
    public class CategoryViewModel:IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}