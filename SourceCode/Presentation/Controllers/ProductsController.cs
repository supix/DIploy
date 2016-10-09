using DomainModel.Services.Products;
using Presentation.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IGetPopularProducts getPopularProducts;

        public ProductsController(IGetPopularProducts getPopularProducts)
        {
            this.getPopularProducts = getPopularProducts;
        }

        /// <summary>
        ///     Returns a sorted paginated list of most "popular" products
        /// </summary>
        /// <param name="page">Requested page</param>
        /// <param name="pageLength">Length of the page</param>
        /// <returns>The view containing the requested page.</returns>       
        public ActionResult Index(int page = 0, int pageLength = 10)
        {
            var products = getPopularProducts.Get(page, pageLength);

            var productsViewModel = new PagedProductsViewModel()
            {
                Products = (from p in products.Products
                            select new ProductViewModel()
                            {
                                Id = p.Id,
                                Description = p.Description
                            }).ToArray(),
                TotalItems = products.TotalItems
            };

            return View(productsViewModel);
        }
    }
}