using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class Love : EntityBase<Guid>
    {
        public Guid TourPackageId { get; set; }
        public virtual TourPackage TourPackage { get; set; }

        public Guid AuthorId { get; set; }
    }
}
