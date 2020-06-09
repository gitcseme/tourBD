using System;
using Microsoft.AspNetCore.Identity;

namespace tourBD.Membership.Entities
{
    public class UserToken
        : IdentityUserToken<Guid>
    {
        public UserToken()
            : base()
        {

        }
    }
}
