using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.CompanyModels
{
    public class CreatePackageViewModel : LayoutBaseModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Division { get; set; }

        public string PackageCode { get; set; }

        public string Availability { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price must be included")]
        public double Price { get; set; }

        public double Discount { get; set; }

        [Required]
        public int Days { get; set; }

        public List<Spot> Spots { get; set; } = new List<Spot>();

        public Guid CompanyId { get; set; }
    }
}
