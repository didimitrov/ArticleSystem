using System.Collections.Generic;
using System.Net.Mime;
using ArticleSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArticleSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ArticleSystem.Data.ApplicationDbContext context)
        {
            if (context.Articles.Any())
            {
                return;
            }
            //  This method will be called after migrating to the latest version.
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           this.SeedCategoriesWithProducts(context);
            //this.SeedTags(context);
            //this.SeedRoles(context);
            //this.SeedUsers(context);
        }

        private void SeedCategoriesWithProducts(ApplicationDbContext context)
        {
            IList<Category> categories = new List<Category>()
            {
                new Category() {Name = "Fun"},
                new Category() {Name = "IT"},
                new Category() {Name = "Game"},

            };
        }
    }
}
