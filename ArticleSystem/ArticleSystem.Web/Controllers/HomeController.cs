using System;
using System.Drawing;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using ArticleSystem.Common.Repository;
using ArticleSystem.Models;
using ArticleSystem.Web.Models.Home;
using AutoMapper.QueryableExtensions;

namespace ArticleSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<Article> _articles;

        public HomeController(IRepository<Article> articles)
        {
            this._articles = articles;
        }

        public ActionResult Index()
        {
            if (this.HttpContext==null)
            {
                return null;
            }
            if (HttpContext.Cache["HomePageProducts"]==null)
            {
                var products =
                    this._articles.All().OrderByDescending(x => x.Id).Take(3).ProjectTo<ArticleHomeViewModel>();

                this.HttpContext.Cache.Add("HomePageProducts", products.ToList(), null, DateTime.Now.AddHours(1),
                    TimeSpan.Zero, CacheItemPriority.Default, null);
            }
            var categories = this.Data.Categories.All().Project().To<CategoryViewModel>();
            this.ViewData.Add("Categories", categories);

            return View(HttpContext.Cache["HomePageProducts"]);
        }

        //public ActionResult GetImage(int id)
        //{
        //    var img = this.Data.Articles.All().FirstOrDefault(x => x.Id == id).Image;
        //    return this.File(img, "Image/jpg");
        //}
    }
}