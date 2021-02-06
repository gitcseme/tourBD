using Microsoft.AspNetCore.Http;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.CompanyModels
{
    public class EditCompanyViewModel
    {
        public IFormFile ImageFile { get; set; }

        public IFormFile LogoFile { get; set; }

        public Company Company { get; set; }
    }
}
