# Overview

Application start-up helper to initialize things like an IoC container, register mapping information or run a task.

[![Build status](https://ci.appveyor.com/api/projects/status/lk092y48a2b9f8ys)](https://ci.appveyor.com/project/LoreSoft/kickstart)

| Package | Version |
| :--- | :--- |
| [KickStart](https://www.nuget.org/packages/KickStart/) |  [![KickStart](https://img.shields.io/nuget/v/KickStart.svg)](https://www.nuget.org/packages/KickStart/) |
| [KickStart.Autofac](https://www.nuget.org/packages/KickStart.Autofac/) |  [![KickStart.Autofac](https://img.shields.io/nuget/v/KickStart.Autofac.svg)](https://www.nuget.org/packages/KickStart.Autofac/) |
| [KickStart.AutoMapper](https://www.nuget.org/packages/KickStart.AutoMapper/) |  [![KickStart.AutoMapper](https://img.shields.io/nuget/v/KickStart.AutoMapper.svg)](https://www.nuget.org/packages/KickStart.AutoMapper/) |
| [KickStart.DependencyInjection](https://www.nuget.org/packages/KickStart.DependencyInjection/) |  [![KickStart.DependencyInjection](https://img.shields.io/nuget/v/KickStart.DependencyInjection.svg)](https://www.nuget.org/packages/KickStart.DependencyInjection/) |
| [KickStart.MongoDB](https://www.nuget.org/packages/KickStart.MongoDB/) |  [![KickStart.MongoDB](https://img.shields.io/nuget/v/KickStart.MongoDB.svg)](https://www.nuget.org/packages/KickStart.MongoDB/) |
| [KickStart.Ninject](https://www.nuget.org/packages/KickStart.Ninject/) |  [![KickStart.Ninject](https://img.shields.io/nuget/v/KickStart.Ninject.svg)](https://www.nuget.org/packages/KickStart.Ninject/) |
| [KickStart.SimpleInjector](https://www.nuget.org/packages/KickStart.SimpleInjector/) |  [![KickStart.SimpleInjector](https://img.shields.io/nuget/v/KickStart.SimpleInjector.svg)](https://www.nuget.org/packages/KickStart.SimpleInjector/) |
| [KickStart.Unity](https://www.nuget.org/packages/KickStart.Unity/) |  [![KickStart.Unity](https://img.shields.io/nuget/v/KickStart.Unity.svg)](https://www.nuget.org/packages/KickStart.Unity/) |

## Download

The KickStart library is available on nuget.org via package name `KickStart`.

To install KickStart, run the following command in the Package Manager Console

    PM> Install-Package KickStart

More information about NuGet package available at
<https://nuget.org/packages/KickStart>

## Development Builds

Development builds are available on the myget.org feed.  A development build is promoted to the main NuGet feed when it's determined to be stable. 

In your Package Manager settings add the following package source for development builds:
<http://www.myget.org/F/loresoft/>

## Features

- Run tasks on application start-up
- Extension model to add library specific start up tasks
- Common IoC container adaptor based on `IServiceProvider`
- Singleton instance of an application level IoC container `Kick.ServiceProvider`
