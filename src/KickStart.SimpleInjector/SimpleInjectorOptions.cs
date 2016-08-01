using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorOptions
    {
        public Action<Container> InitializeContainer { get; set; }
    }
}
