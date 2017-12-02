# Inyector

What is Injector?

Injector is a library for auto configure our dependencies regardless of the technology we use, that is, it is not a dependency injection engine.

It is simply an abstraction layer to configure our objects no matter what technology we use as an injection engine.

You can use Injector with your favorite libraries like Asp.Net Core DI, Autofac, Ninject and others ...


[![Build status](https://ci.appveyor.com/api/projects/status/j7f6vfv3s4nwwak6?svg=true)](https://ci.appveyor.com/project/davidrevoledo/inyector)
[![CodeFactor](https://www.codefactor.io/repository/github/davidrevoledo/inyector/badge)](https://www.codefactor.io/repository/github/davidrevoledo/inyector)

### Installation
Grab the latest Inyector NuGet package and install in your solution. https://www.nuget.org/packages/Inyector/
```sh
PM > Install-Package Inyector 
NET CLI - dotnet add package Inyector 
paket add Inyector --version 0.1.1	
```

### Usage

#### Raw
To use injector directly you should call the 'C# InyectorStartup ' class like this :
'''c#
InyectorStartup.Init(c =>
            {
                c.Scan(typeof(AnyClass).Assembly)
                    .AddRuleForNamingConvention((type, interf) => services.AddSingleton(interf, type))
                    .AddRule((type, inter)=> services.RegisterType(inter, type));
            });
'''

'C# Init ' methods take as param an action delegate with a Inyector Configurations to apply your custom configurations.
What options do you have here ? 






### Licensing
Inyector is licensed under the MIT License

### Development
Want to contribute? Great!



