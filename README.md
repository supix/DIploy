# DIploy

Dependency Injection (DI) is one of the most valuable architectural patterns in software development. It encourages clean design, programming towards interfaces (as opposite to implementations), promotes unit testing, greatly improves extensibility.

Anyway, source code organization is not that trivial when using dependency injection within medium/big software projects. Some questions arise in this case:
 * where have interfaces to be put?
 * where have implementations to be put?
 * where has container configuration to be put?

This contribution shows how to organize a project while using dependency injection, with an example in C# using [Visual Studio CE 2013](https://www.visualstudio.com/it/vs/community/) and [SimpleInjector](https://simpleinjector.org/index.html) DI library.

The concepts described are highly general but, for the sake of concretness, let's imagine to have an e-commerce application that shows to the user a list with the most *popular* products. It is not important whether the application is a web-based application showing the list in the home page, or rather it is a mobile app showing the list as soon as it is started: presentation technology does not matter so much in this context.

