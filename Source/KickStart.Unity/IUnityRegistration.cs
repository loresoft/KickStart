using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public interface IUnityRegistration
    {
        void Register(IUnityContainer container);
    }
}