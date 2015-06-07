using System.Linq;
using System.Web.Mvc;
using ArticleSystem.Common.Repository;
using ArticleSystem.Models;
using ArticleSystem.Web.Models.Home;

namespace ArticleSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Article> _article;

        public HomeController(IRepository<Article> article)
        {
            this._article = article;
        }

        public ActionResult Index()
        {
            //var articles = _article.All().OrderByDescending(article => article.Votes.Count)
            //    .Select(x => new ArticleViewModel
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Price = x.Price,
            //        Url = x.Url
            //    }).ToList();
            //return View(articles);
            return View();
        }


    }
}