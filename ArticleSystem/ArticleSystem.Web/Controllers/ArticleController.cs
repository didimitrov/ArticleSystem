using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ArticleSystem.Common.Repository;
using ArticleSystem.Data;
using ArticleSystem.Models;
using ArticleSystem.Web.Models.Aarticle;
using Microsoft.AspNet.Identity;

namespace ArticleSystem.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IRepository<Article> _article;
        private readonly IRepository<Vote> _vote;
        private readonly IRepository<Comment> _comment; 


        public ArticleController(IRepository<Article> article, IRepository<Vote> vote, IRepository<Comment> comment)
        {
            _article = article;
            _vote = vote;
            _comment = comment;
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
                    Id=x.Id,
                    Price = x.Price,
                    UserCanVote = x.Votes.All(pesho => pesho.VotedById != currentUserId),//todo: Remove this
                    Votes = x.Votes.Count()
                }).FirstOrDefault();
            return View(articleDetailaModel);
        }

        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            

            if (ModelState.IsValid)
            {
                
                var userName = User.Identity.GetUserName();
                var userId = User.Identity.GetUserId();
                _comment.Add(new Comment
                {
                    
                    AuthorId = userId,
                    Content = commentModel.Comment,
                    ArticleId = commentModel.ArticleId
                });
                //db.SaveChanges();
                _comment.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = userName, Content = commentModel.Comment };
                return PartialView("_CommentPartial", viewModel);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
        [HttpPost]
        public ActionResult Vote(int id)
        {

           
            //var article = this._article.All().FirstOrDefault(x => x.Id == id);
            //if (article != null)
            //{
            //    var userId = this.User.Identity.GetUserId();

            //    var vote = new Vote
            //    {
            //        ArticleId = id,
            //        VotedById = userId,
                   
            //    };

            //    this._vote.Add(vote);
            //    this._vote.SaveChanges();

            //    var votes = _article.GetById(id).Votes.Count();

            //    return Content(votes.ToString(CultureInfo.InvariantCulture));
            //}

            //return Content("Error");

            var userId = User.Identity.GetUserId();

            var canVote = !_vote.All().Any(x => x.ArticleId == id && x.VotedById == userId);

            if (canVote)
            {
                _article.GetById(id)
                    .Votes
                    .Add(new Vote
                    {
                        ArticleId = id,
                        VotedById = userId
                    });
                _vote.SaveChanges();
            }

            var votes = _article.GetById(id).Votes.Count();

            return Content(votes.ToString(CultureInfo.InvariantCulture));
        }
    }
}