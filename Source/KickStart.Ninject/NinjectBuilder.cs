using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace KickStart.Ninject
{
    public class NinjectBuilder : INinjectBuilder
    {
        private readonly NinjectOptions _options;

        public NinjectBuilder(NinjectOptions options)
        {
            _options = options;
        }

        public INinjectBuilder Settings(INinjectSettings settings)
        {
            _options.Settings = settings;
            return this;
        }

        public INinjectBuilder Initialize(Action<IKernel> initializer)
        {
            _options.InitializeKernel = initializer;
            return this;
        }

    }
}
