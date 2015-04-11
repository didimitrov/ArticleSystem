using System.Data.Entity;
using ArticleSystem.Data.Migrations;
using ArticleSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArticleSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Vote> Votes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
