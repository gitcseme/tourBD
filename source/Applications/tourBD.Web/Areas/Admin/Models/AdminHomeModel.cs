using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Areas.Admin.Models
{
    public class AdminHomeModel
    {
        public int TotalRegisteredUsers { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalPosts { get; set; }
        public int TotalPackages { get; set; }

        public Dictionary<string, List<Person>> Authorities { get; set; } = new Dictionary<string, List<Person>>();
    }

    public class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
