# Inyector

What is Injector?

Injector is a library for auto configure our dependencies regardless of the technology we use, that is, ***it is not*** a dependency injection engine.

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

### How to use
           - Scan (You declare the assemblies to execute Inyector)
           
           - Modes (The way to declare inyection engine without repeat code, you can have many modes as you want with a Name and a    action of how to resolve inyection)

#### AspNetCore

```c#
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // use injector
            services.UseInjector(configurations =>
            {
                configurations.Scan(typeof(Startup).Assembly)
                    .DefaultMode(services, ServiceLifetime.Singleton)
                    .AddRuleForNamingConvention(ServiceLifetime.Singleton);
            });
        }
```

#### Raw
To use injector directly you should call the ```C# InyectorStartup ``` class like this :
```c#
InyectorStartup.Init(c =>
            {
                c.Scan(typeof(AnyClass).Assembly)
                    .AddRuleForNamingConvention((type, interf) => services.AddSingleton(interf, type))
                    .AddRule((type, inter)=> services.RegisterType(inter, type));
            });
```



### Licensing
Inyector is licensed under the MIT License

### Development
Want to contribute? Great!



