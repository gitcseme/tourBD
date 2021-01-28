using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class TourPackage : EntityBase<Guid>
    {
        [MaxLength(30)]
        [Required(ErrorMessage = "Division is required")]
        public string Division { get; set; }

        public string PackageCode { get; set; }

        [MaxLength(15)]
        public string Availability { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price must be included")]
        public double Price { get; set; }
        public double Discount { get; set; }

        [Required]
        public int Days { get; set; }
        
        public List<Spot> Spots { get; set; } = new List<Spot>();
        public List<Love> Loves { get; set; } = new List<Love>();

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}