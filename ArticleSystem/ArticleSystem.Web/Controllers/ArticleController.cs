using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ArticleSystem.Common.Repository;
using ArticleSystem.Data;
using ArticleSystem.Models;
using ArticleSystem.Web.Models.Aarticle;
using ArticleSystem.Web.Models.Home;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;

namespace ArticleSystem.Web.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IRepository<Article> _articles;
        private readonly IRepository<Vote> _vote;
        private readonly IRepository<Comment> _comments;
        //private readonly IRepository<Category> _categories; 


        public ArticleController(IRepository<Article> articles, IRepository<Vote> vote, IRepository<Comment> comments)
        {
            _articles = articles;
            _vote = vote;
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
            //var articleDetailsModel = _article.All().Where(article => article.Id == id)
            //    .Select(x => new ArticleDetailsViewModel
            //    {
            //        Comments = x.Comments.Select(y => new CommentViewModel
            //        {
            //            AuthorUsername = y.User.UserName,
            //            Content = y.Content
            //        }).ToList(),
            //        Description = x.Description,
            //        ImageUrl = x.Url,
            //        Name = x.Name,
            //        Id=x.Id,
            //        Price = x.Price,
            //        UserCanVote = x.Votes.All(pesho => pesho.VotedById != currentUserId),//todo: Remove this
            //        Votes = x.Votes.Count()
            //    }).FirstOrDefault();

            var articleDetailsModel =
                this._articles.All()
                .Where(x => x.Id == id)
                .ProjectTo<ArticleDetailsViewModel>()
                .FirstOrDefault();

            if (articleDetailsModel == null)
            {
                return HttpNotFound();
            }

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
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                var userId = User.Identity.GetUserId();
                _comments.Add(new Comment
                {
                    //Id = commentModel.Id,
                    AuthorId = userId,
                    Content = commentModel.Comment,
                    ArticleId = commentModel.ArticleId,
                    CreatedAt = DateTime.Now
                });

                var article = _articles.All().FirstOrDefault(x => x.Id == commentModel.ArticleId);
               
               _comments.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = userName, Content = commentModel.Comment };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        [HttpPost]
        public ActionResult Vote(int id)
        {
            var userId = User.Identity.GetUserId();

            var canVote = !_vote.All().Any(x => x.ArticleId == id && x.VotedById == userId);

            if (canVote)
            {
                _articles.GetById(id)
                    .Votes
                    .Add(new Vote
                    {
                        ArticleId = id,
                        VotedById = userId
                    });
                _vote.SaveChanges();
            }

            var votes = _articles.GetById(id).Votes.Count();

            return Content(votes.ToString(CultureInfo.InvariantCulture));
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