using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;

namespace tourBD.Forum.Entities
{
    public class Like : EntityBase<Guid>
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid AuthorId { get; set; }
    }
}
