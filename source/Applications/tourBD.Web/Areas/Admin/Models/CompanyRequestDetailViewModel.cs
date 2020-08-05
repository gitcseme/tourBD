using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Areas.Admin.Models
{
    public class CompanyRequestDetailViewModel
    {
        public string UserImageUrl { get; set; }
        public string UserName { get; set; }
        public string CompanyRequestId { get; set; }
        public string Description { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestStatus { get; set; }
    }
}
