using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.PostModels
{
    public class CreatePostViewModel : LayoutBaseModel
    {
        public string UserId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }

        [Required(ErrorMessage = "Post can't be empty")]
        public string Message { get; set; }
    }
}
