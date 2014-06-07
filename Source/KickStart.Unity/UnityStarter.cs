using System;

namespace KickStart.Unity
{
    public class UnityStarter : IKickStarter
    {
        private readonly UnityOptions _options;

        public UnityStarter(UnityOptions options)
        {
            _options = options;
        }

        public void Run(Context context)
        {

        }
    }
}