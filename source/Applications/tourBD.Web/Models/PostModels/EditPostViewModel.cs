using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.PostModels
{
    public class EditPostViewModel : LayoutBaseModel
    {
        public string PostId { get; set; }
        public string Message { get; set; }
    }
}
