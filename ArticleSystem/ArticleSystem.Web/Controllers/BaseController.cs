using System;
using System.Linq;
using System.Web.Mvc;
//using ArticleSystem.Data.Uow;
using System.Web.Routing;
using ArticleSystem.Data;
using ArticleSystem.Models;

namespace ArticleSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData Data;

        public BaseController(IUowData data)
        {
            this.Data = data;
        }

        public BaseController()
            : this(new UowData())
        {
        }

        protected ApplicationUser UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.UserProfile = this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}