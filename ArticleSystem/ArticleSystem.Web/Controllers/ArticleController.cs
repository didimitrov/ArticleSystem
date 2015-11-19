using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using ArticleSystem.Common.Repository;
using ArticleSystem.Data;
using ArticleSystem.Models;
using ArticleSystem.Web.Models.Aarticle;
using ArticleSystem.Web.Models.Home;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;

namespace ArticleSystem.Web.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IRepository<Article> _articles;
       // private readonly IRepository<Vote> _vote;
        private readonly IRepository<Comment> _comments;
        //private readonly IRepository<Category> _categories; 


        public ArticleController(IRepository<Article> articles, IRepository<Comment> comments)
        {
            _articles = articles;
            
            _comments = comments;
        }

        //public ActionResult Index()
        //{
        //    var articles = _article.All().OrderByDescending(article => article.Votes.Count)
        //        .Select(x => new ArticleHomeViewModel
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Price = x.Price,
        //            Url = x.Url,
        //            Description = x.Description
        //            }).ToList();
        //    return View(articles);
           
        //}

        // GET: Article
        public ActionResult Details(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            
            var article = _articles.All().FirstOrDefault(x => x.Id == id);
            var articleDetailsModel = Mapper.Map<ArticleDetailsViewModel>(article);
            
            articleDetailsModel.UserCanVote = article.Votes.All(x => x.VotedById != currentUserId);

            var comments =
                this.Data.Comments.All()
                    .Where(x => x.ArticleId == articleDetailsModel.Id)
                    .ProjectTo<CommentViewModel>()
                    .ToList();

            articleDetailsModel.Comments = comments;

            var categories = this.Data.Categories.All().ProjectTo<CategoryViewModel>();
            this.ViewData.Add("Categories", categories);

            return View(articleDetailsModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            //if (ModelState.IsValid)
            //{
            //    var userName = User.Identity.GetUserName();
            //    var userId = User.Identity.GetUserId();
            //    _comments.Add(new Comment
            //    {
            //        //Id = commentModel.Id,
            //        AuthorId = userId,
            //        Content = commentModel.Comment,
            //        ArticleId = commentModel.ArticleId,
            //        CreatedAt = DateTime.Now
            //    });
            //   _comments.SaveChanges();

            //    var viewModel = new CommentViewModel { AuthorUsername = userName, Content = commentModel.Comment };
            //    return PartialView("_CommentPartial", viewModel);
            //}
            //return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());

            if (commentModel != null && this.ModelState.IsValid)
            {

                var databaseComment = new Comment
                {
                  //  Id=commentModel.Id,
                    User = this.UserProfile,
                    Content = commentModel.Comment,
                    ArticleId = commentModel.ArticleId,
                    CreatedAt = DateTime.Now
                };

                var article = this.Data.Articles.GetById(commentModel.ArticleId);
                if (article == null)
                {
                    throw new HttpException(404, "Product not found!");
                }

                article.Comments.Add(databaseComment);
                this.Data.SaveChanges();
                
               // return PartialView("_ProductCommentsPartial", product.Comments.AsQueryable().Project().To<CommentViewModel>());
                return this.PartialView("_CommentPartial", article.Comments.SingleOrDefault(x=>x.Id==databaseComment.Id).ProjectTo<CommentViewModel>());
            }

            return this.Json("Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Vote(int id)
        {

            var article = _articles.All().SingleOrDefault(x => x.Id == id);

            if (article != null)
            {
                var userHasVoted = article.Votes.Any(x => x.VotedById == this.User.Identity.GetUserId());
                if (!userHasVoted)
                {
                    this.Data.Votes.Add(new Vote
                    {
                        ArticleId = id,
                        VotedById = User.Identity.GetUserId()
                    });
                    this.Data.SaveChanges();
                }

                var votesCount = article.Votes.Count();
                return this.Content(votesCount.ToString());
            }

            return new EmptyResult(); 
        }

        public ActionResult Search(ArticleSearchModel model)
        {
            // To Bind the category drop down in search section
            ViewBag.Categories = this.Data.Categories.All();

            // Get Products
            model.Articles = _articles.All()
                .Where(
                    x =>
                        (model.ProductName == null || x.Name.Contains(model.ProductName))
                        && (model.Price == null || x.Price < model.Price)
                        && (model.Category == null || x.CategoryId == model.Category)
                )
                //.OrderBy(model.Sort + " " + model.SortDir)
                //.Skip((model.Page - 1) * model.PageSize)
                //.Take(model.PageSize)
                .ToList();

            // total records for paging
            model.TotalRecords = _articles.All()
                .Count(x =>
                    (model.ProductName == null || x.Name.Contains(model.ProductName))
                    && (model.Price == null || x.Price < model.Price)
                    && (model.Category == null || x.CategoryId == model.Category)
                );


            return View(model);
        }
    }
}