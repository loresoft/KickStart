using System;

namespace KickStart.Unity
{
    public class UnityBuilder : IUnityBuilder
    {
        private readonly UnityOptions _options;

        public UnityBuilder(UnityOptions options)
        {
            _options = options;
        }
    }
}