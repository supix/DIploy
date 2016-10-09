# Where dependency-injection binding rules have to be put?

Dependency Injection (DI) is one of the most valuable architectural patterns in software development. It encourages clean design, programming towards interfaces (as opposite to implementations), promotes unit testing, greatly improves extensibility. Benefits of adopting DI *way of life* are clearly described by [this article](http://kozmic.net/2012/10/23/ioc-container-solves-a-problem-you-might-not-have-but-its-a-nice-problem-to-have/).

Anyway, source code organization is not that trivial when using dependency injection within medium/big software projects. Some questions arise in this case:
 * where have interfaces to be put?
 * where have implementations to be put?
 * where has container configuration to be put?

I placed such a [StackOverflow question](http://stackoverflow.com/questions/36386467/where-dependency-injection-registrations-have-to-be-put) some times ago. The answers brought me to the right path. This article expands on this topic showing a concrete solution to the problem.

The presented example is written in C# using [Visual Studio CE 2013](https://www.visualstudio.com/it/vs/community/) and [SimpleInjector](https://simpleinjector.org/index.html) as DI library. The presentation layer is based on an ASP.NET-MVC5 architecture, so the composition root role is played by each of the controllers. Anyway, this concepts are general and can be applied whatever composition root you have.

For the sake of being concrete, let's imagine to have an e-commerce application that provides the user with a list of *the most popular products*.

The architecture can be organized as shown in the picture. There is a domain model, a persistence layer, an application database and a presentation layer. Layers have dependencies, as shown by the arrows which represent the *uses* relation.

The *domain model* contains classes and services related to the business process. It has no dependencies on the other modules.

The *persistence layer* contains non-trivial queries to the DB, exposed as a service to other modules (e.g. a query returning the sorted paginated list of products). It depends on the database and on the domain model (e.g. in order to retrieve domain instances from the database).

The *presentation layer* interfaces the application with the user, according to the chosen presentation technology (e.g. web interface, REST service, windows forms, ecc.). It depends on the presentation layer and consumes its services. It directly depends on the db as well: unless the application is very trivial, it is not always rewarding to rigidly shield the database through a layer exposing all the database functionalities.

The source code reproduces this simple modules organizations. You can see the MVC5 presentation layer. Domain model and persistence layers have been added to the Visual Studio solution as two class-libraries. References among projects have been added, so as described by the picture above.

The *most popular products list* is a domain concept. For example, the application might take top-sold products in the last month mixing them with the products most visited by the logged user, possibly removing the out-of-stock products. The place to put such a domain concept cannot be other than the DomainModel: in the `DomainModel/Services/Products` path there is the `IGetPopularProducts` interface. It is based on the domain class `Product` available in `DomainModel/Classes/Products`. Beside the interface, there is also a fake implementation of the service, useful to work without actually having a DB up and running (i.e. to test the GUI routines). When this implementation works, returned products have names starting with "DB...".

The actual (though nevertheless emulated) implementation of the service is in the file `PersistenceLayer/Products/GetPopularProducts_Db.cs`. When this implementation works, returned products have names starting with "Fake...".

So far, we have the service definition (interface) and two service implementations in two different places. The next step is to create a `ProductsController` which is in charge of showing the list of products through an action. The `IGetPopularProducts` is injected into the controller and the view is coded. We skip the details, which are out of scope now. If you run the project, you get an error saying that `ProductsController` has not a default constructor. This is the moment when the DI controller comes into play.

So, let's install the DI library through the following NuGet commands:

```NuGet
Install-Package SimpleInjector.Integration.Web.Mvc Presentation
Install-Package SimpleInjector.Packaging Presentation
Install-Package SimpleInjector.Packaging PersistenceLayer
Install-Package SimpleInjector.Packaging DomainModel
```

The `SimpleInjector.Integration.Web.Mvc` integrates the DI library in the MVC framework, enabling controllers to act as composition roots. The `SimpleInjector.Packaging` library allows to use *package* abstraction within the solution libraries. It is a very useful abstraction, since you can collect your DI rules within a single file. This is the most important step: thanks to this library you can create as many packages as you want and place there the binding rules, where they have most significance.

Let's configure the container. The `PresentationLayer/App_Start/CompositionRoot.cs` contains the following code.

```C#
	// Create the container as usual.
    var container = new Container();
    container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

    // Scan all the referenced assemblies for packages containing DI wiring rules
    var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
    container.RegisterPackages(assemblies);

    // This is an extension method from the integration package.
    container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

#if DEBUG
    container.Verify();
#endif

    DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
```

As you can see, it contains no binding rules, nor it explicitly refers to some binding routine (e.g. `SomeClass.ConfigureBindings()`). The magic is in the following two lines.

```C#
// Scan all the referenced assemblies for packages containing DI wiring rules
var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
container.RegisterPackages(assemblies);
```

This lines scan all the assemblies referenced by the PersistenceLayer project, searching for binding modules, i.e. the classes inherited by ``IPackage``. Their ``RegisterServices()`` overridden method contains the binding rules. Thanks to packages we can spread DI logic throughout the application, with the aim of placing DI rules **as close as possible to implementations**. In our example, the file `DomainModel/Bindings.cs` refers to the fake implementation of the service. Instead, the file `PersistenceLayer/Bindings.cs` refers to the *actual* implementation. As you can see, both bindings are in the same assembly as the implementations. 

The described approach has these pro and cons.

...work in progress

* Pro
  * The implementations can be declared as private.

* Cons
