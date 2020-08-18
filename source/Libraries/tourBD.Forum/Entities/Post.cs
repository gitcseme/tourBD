using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using tourBD.Core;

namespace tourBD.Forum.Entities
{
    public class Post : EntityBase<Guid>
    {
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Post can't be empty")]
        public string Message { get; set; }

        public virtual IList<Comment> Comments { get; set; } = new List<Comment>();
        public virtual IList<Like> Likes { get; set; } = new List<Like>();
    }
}
