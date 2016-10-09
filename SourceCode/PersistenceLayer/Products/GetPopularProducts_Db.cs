using DomainModel.Classes.Products;
using DomainModel.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Products
{
    class GetPopularProducts_Db : IGetPopularProducts
    {
        /// <summary>
        /// This method emulates a DB query, returning products whose name starts with "DB..."
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
                        Description = "DB Apple"
                    }, 
                    new Product() {
                        Id = "1",
                        Description = "DB Cucumber"
                    },
                    new Product() {
                        Id = "2",
                        Description = "DB Potato"
                    },
                    new Product() {
                        Id = "3",
                        Description = "DB Doll"
                    },
                    new Product() {
                        Id = "4",
                        Description = "DB Lighter"
                    },
                    new Product() {
                        Id = "5",
                        Description = "DB Snack"
                    },
                    new Product() {
                        Id = "6",
                        Description = "DB Chair"
                    },
                    new Product() {
                        Id = "7",
                        Description = "DB Chewing gum"
                    },
                    new Product() {
                        Id = "8",
                        Description = "DB Pepsi"
                    },
                    new Product() {
                        Id = "9",
                        Description = "DB Beer"
                    }
                }
            };
        }
    }
}
