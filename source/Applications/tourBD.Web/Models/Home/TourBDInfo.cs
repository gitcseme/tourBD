using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.Home
{
    public class TourBDInfo
    {
        public string OfficialEmail { get; set; }
        public string Logo { get; set; }
        public Developer Developer { get; set; }
    }

    public class Developer
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Education { get; set; }
        public string Institution { get; set; }

    }
}
