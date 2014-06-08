using System;
using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac
{
    public interface IAutofacBuilder
    {
        IAutofacBuilder Options(ContainerBuildOptions buildOptions);
        IAutofacBuilder Initialize(Action<ContainerBuilder> initializer);
        IAutofacBuilder Initialize(Action<IContainer> initializer);
    }
}