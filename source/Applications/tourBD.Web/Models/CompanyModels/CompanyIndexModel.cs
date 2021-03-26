using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.CompanyModels
{
    public class CompanyIndexModel : LayoutBaseModel
    {
        public List<CompanyViewModel> Companies { get; set; } = new List<CompanyViewModel>();
    }
}
