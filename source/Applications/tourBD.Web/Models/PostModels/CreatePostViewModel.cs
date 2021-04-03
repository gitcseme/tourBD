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
        [StringLength(2500, MinimumLength = 20, ErrorMessage = "Post must have minimum 20 and maximum 2500 characters")]
        public string Message { get; set; }
    }
}
