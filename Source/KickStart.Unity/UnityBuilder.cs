using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public class UnityBuilder : IUnityBuilder
    {
        private readonly UnityOptions _options;

        public UnityBuilder(UnityOptions options)
        {
            _options = options;
        }

        public IUnityBuilder Initialize(Action<IUnityContainer> initializer)
        {
            _options.InitializeContainer = initializer;
            return this;
        }
    }
}