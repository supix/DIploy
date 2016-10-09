using SimpleInjector;
using SimpleInjector.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.App_Start
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            //no bindings here, but they could be added for services local to presentation layer
        }
    }
}