using System;
using System.ComponentModel.DataAnnotations;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class Spot : EntityBase<Guid>
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "Spot name can't be empty")]
        public string Name { get; set; }

        public Guid TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }
    }
}