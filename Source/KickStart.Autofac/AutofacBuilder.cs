using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac
{
    public class AutofacBuilder : IAutofacBuilder
    {
        private readonly AutofacOptions _options;

        public AutofacBuilder(AutofacOptions options)
        {
            _options = options;
        }

        public IAutofacBuilder Options(ContainerBuildOptions buildOptions)
        {
            _options.BuildOptions = buildOptions;
            return this;
        }

        public IAutofacBuilder Builder(Action<ContainerBuilder> builder)
        {
            _options.Builder = builder;
            return this;
        }

        public IAutofacBuilder Container(Action<global::Autofac.IContainer> container)
        {
            _options.Container = container;
            return this;
        }

    }
}
