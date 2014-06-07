using System;
using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac
{
    public class AutofacOptions
    {
        public AutofacOptions()
        {
            BuildOptions = ContainerBuildOptions.None;
        }

        public ContainerBuildOptions BuildOptions { get; set; }

        public Action<ContainerBuilder> Builder { get; set; }

        public Action<IContainer> Container { get; set; }
    }
}