using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Web.Models.CompanyModels
{
    public class PackageAjaxViewModel
    {
        private int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public List<TourPackage> Packages { get; set; } = new List<TourPackage>();

        public PackageAjaxViewModel(List<TourPackage> packages, int pageIndex, int PageSize, int TotalRecord)
        {
            PageIndex = pageIndex;
            Packages = packages;

            TotalPages = (int)Math.Ceiling((double)TotalRecord / PageSize);
        }

        public bool PreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool NextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
