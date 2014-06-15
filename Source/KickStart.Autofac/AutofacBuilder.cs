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

        public IAutofacBuilder Builder(Action<ContainerBuilder> initializer)
        {
            _options.InitializeBuilder = initializer;
            return this;
        }

        public IAutofacBuilder Container(Action<IContainer> initializer)
        {
            _options.InitializeContainer = initializer;
            return this;
        }

    }
}
