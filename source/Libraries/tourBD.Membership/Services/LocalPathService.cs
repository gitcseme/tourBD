using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace tourBD.Membership.Services
{
    public class LocalPathService : IPathService
    {
        public string PictureFolder { get; set; }

        public LocalPathService(IConfiguration configuration)
        {
            PictureFolder = configuration.GetValue<string>("PictureFolder");
        }
    }
}
