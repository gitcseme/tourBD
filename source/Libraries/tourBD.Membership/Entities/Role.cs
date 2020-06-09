using Microsoft.AspNetCore.Identity;
using System;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class Role : IdentityRole<Guid>, IEntity<Guid>
    {
        public Role()
            : base()
        {
        }

        public Role(string roleName)
            : base(roleName)
        {
        }
    }
}
