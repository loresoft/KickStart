using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public interface ISimpleInjectorRegistration
    {
        void Register(Container container);
    }
}