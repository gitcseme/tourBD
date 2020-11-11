using System;
using System.Collections.Generic;

namespace tourBD.Web.Models.PostModels
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public DateTime CreationDate { get; set; }

        public string Message { get; set; }
        public bool IsCommentAuthor { get; set; }

        public List<ReplayViewModel> Replays { get; set; } = new List<ReplayViewModel>();
    }
}