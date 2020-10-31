using System;
using System.Collections.Generic;
using System.Text;

namespace tourBD.Membership.Services
{
    public interface IPathService
    {
        string PictureFolder { get; set; }
        string DummyUserImageUrl { get; set; }
        string DummyCompanyImageUrl { get; set; }
    }
}
