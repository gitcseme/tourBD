using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class Company : EntityBase<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        public string CompanyImageUrl { get; set; }
        public string CompanyLogo { get; set; }
        public int Star { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<TourPackage> TourPackages { get; set; } = new List<TourPackage>();
    }
}
