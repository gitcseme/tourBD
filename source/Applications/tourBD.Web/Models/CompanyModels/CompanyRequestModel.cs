using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models.CompanyModels
{
    public class CompanyRequestModel : LayoutBaseModel
    {
        [Required(ErrorMessage = "Description needed")]
        public string Description { get; set; }

        public string OfficialEmail { get; set; }
    }
}
