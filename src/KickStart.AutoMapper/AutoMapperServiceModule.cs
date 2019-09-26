using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KickStart.Services;

namespace KickStart.AutoMapper
{

    /// <summary>
    /// AutoMapper service module registration
    /// </summary>
    /// <seealso cref="KickStart.Services.IServiceModule" />
    public class AutoMapperServiceModule : IServiceModule
    {
        /// <summary>Register service injections with the specified <paramref name="services" /> container.</summary>
        /// <param name="services">The <see cref="T:KickStart.Services.IServiceRegistration"/> container to add the module services to.</param>
        /// <param name="data">The data dictionary shared with all starter modules.</param>
        public void Register(IServiceRegistration services, IDictionary<string, object> data)
        {
            data.TryGetValue(AutoMapperStarter.AutoMapperConfiguration, out var configurationValue);

            var configuration = configurationValue as IConfigurationProvider;
            if (configuration == null)
                return;

            services.RegisterSingleton(configuration);
            services.RegisterSingleton<IMapper>(s => new Mapper(s.GetService<IConfigurationProvider>()));
        }
    }
}
