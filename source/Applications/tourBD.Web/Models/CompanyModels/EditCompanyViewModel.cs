using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.CompanyModels
{
    public class EditCompanyViewModel
    {
        public IFormFile ImageFile { get; set; }

        public Company Company { get; set; }
    }
}
