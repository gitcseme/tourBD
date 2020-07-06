using System;
using System.ComponentModel.DataAnnotations;
using tourBD.Core;

namespace tourBD.Forum.Entities
{
    public class Replay : EntityBase<Guid>
    {
        public Guid AuthorId { get; set; }

        public Guid CommentId { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Replay can't be empty")]
        public string Message { get; set; }
    }
}