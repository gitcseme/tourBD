using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.CompanyModels
{
    public class EditPackageViewModel
    {
        public Guid packageId { get; set; }
        public string MainArea { get; set; }
        public int PackageNumber { get; set; }
        public string Availability { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public int PackageMemberSize { get; set; }
        public List<string> Spots { get; set; } = new List<string>();
    }
}
