using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.CompanyModels
{
    public class EditPackageViewModel
    {
        public Guid packageId { get; set; }
        public string PackageCode { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string Availability { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        [Column(TypeName = "decimal(2, 2)")]
        public double Discount { get; set; }

        [Required]
        public int Days { get; set; }
        public List<string> Spots { get; set; } = new List<string>();
    }
}
