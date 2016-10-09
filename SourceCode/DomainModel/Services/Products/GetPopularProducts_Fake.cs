using DomainModel.Classes.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.Products
{
    class GetPopularProducts_Fake : IGetPopularProducts
    {
		/// <summary>
		/// This method fakes the action of returning actual products. It returns products whose name starts with "Fake..."
		/// </summary>
		/// <param name="page">The requested page</param>
		/// <param name="pageLength">The page length</param>
		/// <returns>The products</returns>
		public PopularProductsResult Get(int page, int pageLength)
        {
            return new PopularProductsResult()
            {
                TotalItems = 12345,
                Products = new Product[] { 
                    new Product() {
                        Id = "0",
                        Description = "Fake Apple"
                    }, 
                    new Product() {
                        Id = "1",
                        Description = "Fake Cucumber"
                    },
                    new Product() {
                        Id = "2",
                        Description = "Fake Potato"
                    },
                    new Product() {
                        Id = "3",
                        Description = "Fake Doll"
                    },
                    new Product() {
                        Id = "4",
                        Description = "Fake Lighter"
                    },
                    new Product() {
                        Id = "5",
                        Description = "Fake Snack"
                    },
                    new Product() {
                        Id = "6",
                        Description = "Fake Chair"
                    },
                    new Product() {
                        Id = "7",
                        Description = "Fake Chewing gum"
                    },
                    new Product() {
                        Id = "8",
                        Description = "Fake Pepsi"
                    },
                    new Product() {
                        Id = "9",
                        Description = "Fake Beer"
                    }
                }
            };
        }
    }
}
