using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.CompanyModels
{
    public class CompanyViewModel
    {
        public string Name { get; set; }
        public int Packages { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyId { get; set; }
    }
}
