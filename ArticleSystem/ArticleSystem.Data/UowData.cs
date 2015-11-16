using System;
using System.Collections.Generic;
using System.Data.Entity;
using ArticleSystem.Common.Repository;
using ArticleSystem.Models;

namespace ArticleSystem.Data
{
        public class UowData : IUowData
        {
            private readonly DbContext _context;
            private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

            public UowData()
                : this(new ApplicationDbContext())
            {
            }

            public UowData(DbContext context)
            {
                this._context = context;
            }

            public IRepository<Article> Articles
            {
                get { return this.GetRepository<Article>(); }
            }

            public IRepository<Category> Categories
            {
                get { return this.GetRepository<Category>(); }
            }

            //public IRepository<Manufacturer> Manufacturers
            //{
            //    get { return this.GetRepository<Manufacturer>(); }
            //}

            public IRepository<Comment> Comments
            {
                get { return this.GetRepository<Comment>(); }
            }

            public IRepository<Vote> Votes
            {
                get { return this.GetRepository<Vote>(); }
            }

            public IRepository<ApplicationUser> Users
            {
                get { return this.GetRepository<ApplicationUser>(); }
            }

            public int SaveChanges()
            {
                return this._context.SaveChanges();
            }

            public void Dispose()
            {
                this._context.Dispose();
            }

            private IRepository<T> GetRepository<T>() where T : class
            {
                if (!this._repositories.ContainsKey(typeof(T)))
                {
                    var type = typeof(GenericRepository<T>);

                    this._repositories.Add(typeof(T), Activator.CreateInstance(type, this._context));
                }

                return (IRepository<T>)this._repositories[typeof(T)];
            }
        }

       
    }

