using System;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorBuilder : ISimpleInjectorBuilder
    {
        private readonly SimpleInjectorOptions _options;

        public SimpleInjectorBuilder(SimpleInjectorOptions options)
        {
            _options = options;
        }
    }
}