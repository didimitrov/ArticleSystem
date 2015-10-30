using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleSystem.Models
{
    public class Category
    {
        private ICollection<Article> articles;

        public Category()
        {
            this.articles = new HashSet<Article>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Article> Articles
        {
            get { return articles; }
            set { articles = value; }
        }
    }
}
