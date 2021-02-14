using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;

namespace tourBD.Web.Models.CompanyModels
{
    public class PackageSortViewModel
    {
        public int TotalRecords { get; set; }
        private int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<TourPackage> Packages { get; set; } = new List<TourPackage>();

        public BangladeshDivisions BangladeshDivision { get; set; }
        public bool PriceUP { get; set; }
        public bool PriceDN { get; set; }
        public bool LoveUP { get; set; }
        public bool LoveDN { get; set; }

        public PackageSortViewModel()
        {
            PageIndex = 1;
            PageSize = 5;
            BangladeshDivision = BangladeshDivisions.ALL;
            PriceUP = PriceDN = false;
            LoveUP = LoveDN = false;
        }

        public PackageSortViewModel(List<TourPackage> Packages, int PageIndex, int PageSize, int TotalRecords)
        {
            this.PageIndex = PageIndex;
            this.Packages = Packages;
            this.TotalRecords = TotalRecords;

            TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
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
