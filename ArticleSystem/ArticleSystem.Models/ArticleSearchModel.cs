using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSystem.Models
{
    public class ArticleSearchModel
    {
        public int Id { get; set; }
       [Display(Name = "Product Name")]
        public String ProductName { get; set; }

        [Display(Name = "Price (Max.)")]
        public Decimal? Price { get; set; }

        [Display(Name = "Category")]
        public Int32? Category { get; set; }

        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public String Sort { get; set; }
        public String SortDir { get; set; }
        public Int32 TotalRecords { get; set; }
        public List<Article> Articles { get; set; }

        public ArticleSearchModel()
        {
            Page = 1;
            PageSize = 5;
            Sort = "ProductId";
            SortDir = "DESC";
        }
    }
}
