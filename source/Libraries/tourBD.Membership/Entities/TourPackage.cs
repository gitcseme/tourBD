using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class TourPackage : EntityBase<Guid>
    {
        [Required(ErrorMessage = "Main area is required")]
        public string MainArea { get; set; }
        public string PackageCode { get; set; }
        public string Availability { get; set; }
        [Required(ErrorMessage = "Price must be included")]
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Days { get; set; }
        public List<Spot> Spots { get; set; } = new List<Spot>();

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}