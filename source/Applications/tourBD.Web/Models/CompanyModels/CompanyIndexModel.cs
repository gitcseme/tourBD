using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.CompanyModels
{
    public class CompanyIndexModel
    {
        public List<Company> Companies { get; set; } = new List<Company>();
        public bool HasPendingRequest { get; set; }
    }
}
