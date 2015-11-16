using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Models
{
    public class Article
    {
        private ICollection<Comment> _comments;
        private ICollection<Vote> _votes;

       public Article()
        {
            _comments=new HashSet<Comment>();
            _votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Url { get; set; }
        //[DataType(DataType.Upload)]
        //public byte[] Image { get; set; }

        public bool IsAvalible { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return _votes; }
            set { _votes = value; }
        }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public String Thumb { get; set; }


    }

    
}
