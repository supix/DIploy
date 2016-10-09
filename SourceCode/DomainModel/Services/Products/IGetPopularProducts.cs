using DomainModel.Classes.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.Products
{
    public interface IGetPopularProducts
    {
        PopularProductsResult Get(int page, int pageLength);
    }

    public class PopularProductsResult {
        public Product[] Products { get; set; }
        public int TotalItems { get; set; }
    }
}
