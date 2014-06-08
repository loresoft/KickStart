using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public interface IUnityBuilder
    {
        IUnityBuilder Initialize(Action<IUnityContainer> initializer);

    }
}