using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class TourPackage : EntityBase<Guid>
    {
        [MaxLength(30)]
        [Required(ErrorMessage = "Main area is required")]
        public string Division { get; set; }

        [Required]
        public string PackageCode { get; set; }

        [MaxLength(15)]
        [Required]
        public string Availability { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price must be included")]
        public double Price { get; set; }
        public double Discount { get; set; }

        [Required]
        public int Days { get; set; }
        public List<Spot> Spots { get; set; } = new List<Spot>();

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}