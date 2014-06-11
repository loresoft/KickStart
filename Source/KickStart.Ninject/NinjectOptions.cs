using System;
using Ninject;

namespace KickStart.Ninject
{
    public class NinjectOptions
    {
        public INinjectSettings Settings { get; set; }
        public Action<IKernel> InitializeKernel { get; set; }
    }
}