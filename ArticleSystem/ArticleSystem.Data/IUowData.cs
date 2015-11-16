using ArticleSystem.Common.Repository;
using ArticleSystem.Models;

namespace ArticleSystem.Data
{
    public interface IUowData
    {
        IRepository<Article> Articles { get; }

        IRepository<Category> Categories { get; }

      //  IRepository<Manufacturer> Manufacturers { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Vote> Votes { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
