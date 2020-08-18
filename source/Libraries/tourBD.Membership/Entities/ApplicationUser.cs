using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using tourBD.Core;

namespace tourBD.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsVarified { get; set; }

        public ApplicationUser()
                    : base()
        {

        }

        internal ApplicationUser(string userName)
            : base(userName)
        {

        }

        public ApplicationUser(string userName, string phoneNumber, string email)
            : base(userName)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public ApplicationUser(string userName, string fullName, string phoneNumber, string email)
            : this(userName, phoneNumber, email)
        {
            FullName = fullName;
        }

        public ApplicationUser(string userName, string fullName, string mobileNumber, string email, string imageUrl)
            : this(userName, fullName, mobileNumber, email)
        {
            ImageUrl = imageUrl;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            var hashCode = -582740416;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }
    }
}
