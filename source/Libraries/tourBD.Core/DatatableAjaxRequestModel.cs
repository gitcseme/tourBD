using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace tourBD.Core
{
    public class DatatableAjaxRequestModel
    {
        public DatatableAjaxRequestModel(HttpRequest request)
        {
            Request = request;
            PageSize = Convert.ToInt32(Request.Query["length"]);
            PageIndex = (Convert.ToInt32(Request.Query["start"]) / PageSize) + 1;
            SearchText = Request.Query["search[value]"].FirstOrDefault().Trim();
            OrderDirection = Request.Query["order[0][dir]"];
        }

        public HttpRequest Request { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public string SearchText { get; }
        public string OrderDirection { get; }

        public string SortColumnName {
            get
            {
                int sortColumnindex = Convert.ToInt32(Request.Query["order[0][column]"]);
                string sortColumnName = "columns[" + sortColumnindex + "][name]";
                return Request.Query[sortColumnName];
            }
        }
    }
}
