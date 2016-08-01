using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public class UnityOptions
    {
        public Action<IUnityContainer> InitializeContainer { get; set; }

    }
}
