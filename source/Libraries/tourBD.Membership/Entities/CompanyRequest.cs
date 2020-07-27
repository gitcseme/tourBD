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
        public string Description { get; set; }

        public DateTime RequestDate { get; set; }

        public CompanyRequestStatus RequestStatus { get; set; }
    }
}
