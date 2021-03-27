using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.PostModels
{
    public class ForumModel : LayoutBaseModel
    {
        private int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();

        public ForumModel(List<PostViewModel> Posts, int PageIndex, int PageSize, int TotalRecord)
        {
            this.PageIndex = PageIndex;
            this.Posts = Posts;

            TotalPages = (int)Math.Ceiling((double)TotalRecord / PageSize);
        }

        public bool PreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool NextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
