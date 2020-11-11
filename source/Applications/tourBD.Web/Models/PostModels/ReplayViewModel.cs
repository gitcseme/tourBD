using System;

namespace tourBD.Web.Models.PostModels
{
    public class ReplayViewModel
    {

        public Guid ReplayId { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }

        public Guid CommentId { get; set; }
        public DateTime CreationDate { get; set; }

        public string Message { get; set; }
        public bool IsReplayAuthor { get; set; }
    }
}