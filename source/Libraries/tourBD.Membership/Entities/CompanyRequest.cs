using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using tourBD.Core;
using tourBD.Membership.Enums;

namespace tourBD.Membership.Entities
{
    public class CompanyRequest : EntityBase<Guid>
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Request description is needed")]
        [MaxLength(800)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string RequestStatus { get; set; }
    }
}
