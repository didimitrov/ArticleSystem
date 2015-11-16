using AutoMapper;

namespace ArticleSystem.Web.Infrastructure.Mapping
{
    interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}
