using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Forum.Entities;

namespace tourBD.Web.Models.PostModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public bool IsLikedBy { get; set; }
    }
}
