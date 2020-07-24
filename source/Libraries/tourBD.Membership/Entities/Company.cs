﻿using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class Company : EntityBase<Guid>
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string CompanyImageUrl { get; set; }
        public int Reputation { get; set; }

        public IList<TourPackage> TourPackages { get; set; } = new List<TourPackage>();
    }
}
