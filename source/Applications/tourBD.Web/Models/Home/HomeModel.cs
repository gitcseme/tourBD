using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.Home
{
    public class HomeModel : LayoutBaseModel
    {
        public List<PostModel> Posts { get; set; }
        public List<CompanyModel> Companies { get; set; }
    }
}
