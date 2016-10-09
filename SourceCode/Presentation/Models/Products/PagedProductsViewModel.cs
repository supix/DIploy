using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models.Products
{
    public class PagedProductsViewModel
    {
        public ProductViewModel[] Products { get; set; }
        public int TotalItems { get; set; }
    }
}