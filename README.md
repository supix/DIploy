# DIploy

Dependency Injection (DI) is one of the most valuable architectural patterns in software development. It encourages clean design, programming towards interfaces (as opposite to implementations), promotes unit testing, greatly improves extensibility. Benefits of adopting DI *way of life* are clearly described by [this article](http://kozmic.net/2012/10/23/ioc-container-solves-a-problem-you-might-not-have-but-its-a-nice-problem-to-have/).

Anyway, source code organization is not that trivial when using dependency injection within medium/big software projects. Some questions arise in this case:
 * where have interfaces to be put?
 * where have implementations to be put?
 * where has container configuration to be put?

Let's see a possible way to organize a project while using DI, with an example in C# using [Visual Studio CE 2013](https://www.visualstudio.com/it/vs/community/) and [SimpleInjector](https://simpleinjector.org/index.html) DI library.

The concepts described are highly general but, for the sake of concretness, let's imagine to have an e-commerce application that provides the user with a list of the most *popular* products.

The architecture can be organized as shown in the picture. There is a domain model, a persistence layer, an application database and a presentation layer. Layers have dependencies, as shown by the arrows which represent the *uses* relation.

The *domain model* contains classes and services related to the business process. It has no dependencies on the other modules.

The *persistence layer* contains non-trivial queries to the DB, exposed as a service to other modules (e.g. a query returning the sorted paginated list of products). It depends on the database and on the domain model (e.g. in order to retrieve domain instances from the database).

The *presentation layer* interfaces the application with the user, according to the chosen presentation technology (e.g. web interface, REST service, windows forms, ecc.). It depends on the presentation layer and consumes its services. It depends on the db as well, directly, since it is not always rewarding to rigidly shield the database through a layer exposing all the database functionalities.

The source code reproduces this simple modules organizations. It it based on a ASP.NET MVC5 project, acting as the presentation layer. Domain model and persistence layers have been added to the Visual Studio solution as two class-libraries. References among projects have been added, so as described by the picture above.