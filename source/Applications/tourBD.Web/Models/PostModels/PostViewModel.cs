using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.PostModels
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }

        public string CreationDate { get; set; }
        public string Message { get; set; }
        public int Likes { get; set; }
        public bool IsLikedBy { get; set; }
        public bool IsPostAuthor { get; set; }

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
