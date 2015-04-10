using System.Linq;
using System.Web.Mvc;
using ArticleSystem.Common.Repository;
using ArticleSystem.Models;
using ArticleSystem.Web.Models.Aarticle;
using Microsoft.AspNet.Identity;

namespace ArticleSystem.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IRepository<Article> _article;

        public ArticleController(IRepository<Article> article)
        {
            _article = article;
        }

        // GET: Article
        public ActionResult Details(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var articleDetailaModel = _article.All().Where(article => article.Id == id)
                .Select(x => new ArticleDetailsViewModel
                {
                    Comments = x.Comments.Select(y => new CommentViewModel
                    {
                        AuthorUsername = y.User.UserName,
                        Content = y.Content
                    }).ToList(),
                    Description = x.Description,
                    ImageUrl = x.Url,
                    Name = x.Name,
                    Price = x.Price,
                    UserCanVote = x.Votes.All(pesho => pesho.VotedById != currentUserId),//todo: Remove this
                    Votes = x.Votes.Count()
                }).FirstOrDefault();
            return View(articleDetailaModel);
        }
    }
}