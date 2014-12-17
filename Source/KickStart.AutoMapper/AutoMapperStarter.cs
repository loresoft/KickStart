using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace KickStart.AutoMapper
{
    /// <summary>
    /// A KickStart extension to initialize AutoMapper.
    /// </summary>
    public class AutoMapperStarter : IKickStarter
    {
        private readonly AutoMapperOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperStarter"/> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public AutoMapperStarter(AutoMapperOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var profiles = context.GetInstancesAssignableFrom<Profile>();

            Mapper.Initialize(config =>
            {
                foreach (var profile in profiles)
                {
                    Logger.Trace()
                        .Logger<AutoMapperStarter>()
                        .Message("AutoMapper Profile: {0}", profile)
                        .Write();

                    config.AddProfile(profile);
                }

                if (_options.Initialize != null)
                    _options.Initialize(config);
            });


            if (_options.Validate)
                Mapper.AssertConfigurationIsValid();
        }
    }
}
