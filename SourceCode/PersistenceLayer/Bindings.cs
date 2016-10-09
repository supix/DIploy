using SimpleInjector;
using SimpleInjector.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DomainModel.Services.Products.IGetPopularProducts,
                PersistenceLayer.Products.GetPopularProducts_Db>(Lifestyle.Scoped);
        }
    }
}
