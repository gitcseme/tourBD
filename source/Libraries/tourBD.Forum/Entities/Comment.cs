using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using tourBD.Core;

namespace tourBD.Forum.Entities
{
    public class Comment : EntityBase<Guid>
    {
        public Guid PostId { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Comment can't be empty")]
        public string Message { get; set; }

        public IList<Replay> Replays { get; set; } = new List<Replay>();
    }
}
