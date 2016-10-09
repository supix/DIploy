using SimpleInjector.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace DomainModel
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
			//container.Register<DomainModel.Services.Products.IGetPopularProducts,
            //    DomainModel.Services.Products.GetPopularProducts_Fake>(Lifestyle.Scoped);
        }
    }
}
