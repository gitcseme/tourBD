using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tourBD.Web.Models
{
    public class RegistrationFormModel
    {
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        public string Address { get; set; }
    }
}
