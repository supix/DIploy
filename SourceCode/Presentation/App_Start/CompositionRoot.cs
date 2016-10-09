using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Linq;

namespace Presentation
{
    public class CompositionRoot
    {
        public static void Configure()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Scan all the referenced assemblies for packages containing DI wiring rules
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            container.RegisterPackages(assemblies);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}