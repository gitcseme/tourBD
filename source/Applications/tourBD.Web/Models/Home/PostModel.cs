using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.Home
{
    public class PostModel
    {
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public string Message { get; set; }
        public string PostId { get; set; }
    }
}
