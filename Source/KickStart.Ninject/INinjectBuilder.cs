using System;
using Ninject;

namespace KickStart.Ninject
{
    public interface INinjectBuilder
    {
        INinjectBuilder Settings(INinjectSettings settings);
        INinjectBuilder Kernal(Action<IKernel> initializer);
    }
}