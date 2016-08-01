using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public interface IUnityBuilder
    {
        IUnityBuilder Container(Action<IUnityContainer> initializer);

    }
}