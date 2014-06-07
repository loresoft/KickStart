using System;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorStarter : IKickStarter
    {
        private readonly SimpleInjectorOptions _options;

        public SimpleInjectorStarter(SimpleInjectorOptions options)
        {
            _options = options;
        }

        public void Run(Context context)
        {
        }
    }
}