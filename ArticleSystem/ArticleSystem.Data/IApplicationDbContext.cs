using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ArticleSystem.Models;

namespace ArticleSystem.Data
{
    public interface IApplicationDbContext: IDisposable
    {
        IDbSet<Article> Articles { get; set; }

      

        IDbSet<Comment> Comments { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
