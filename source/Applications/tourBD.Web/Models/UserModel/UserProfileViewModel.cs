using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.UserModel
{
    public class UserProfileViewModel: LayoutBaseModel
    {
        public ApplicationUser User { get; set; }
        public List<Company> Companies { get; set; }
    }
}
