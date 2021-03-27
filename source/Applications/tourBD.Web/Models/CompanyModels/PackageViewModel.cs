using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.CompanyModels
{
    public class PackageViewModel : LayoutBaseModel
    {
        public Company Company { get; set; }
        public TourPackage Package { get; set; }

        public bool IsLoved { get; set; }
        public int Loves { get; set; }
    }
}
