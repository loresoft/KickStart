using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorBuilder : ISimpleInjectorBuilder
    {
        private readonly SimpleInjectorOptions _options;

        public SimpleInjectorBuilder(SimpleInjectorOptions options)
        {
            _options = options;
        }

        public ISimpleInjectorBuilder Initialize(Action<Container> initializer)
        {
            _options.InitializeContainer = initializer;
            return this;
        }
    }
}