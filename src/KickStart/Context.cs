using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using KickStart.Services;
#if PORTABLE
using Stopwatch = KickStart.Portability.Stopwatch;
#else
using Stopwatch = System.Diagnostics.Stopwatch;
#endif

namespace KickStart
{
    /// <summary>
    /// The KickStart running context.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Context" /> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="data">The data dictionary shared with all starter modules.</param>
        /// <param name="logWriter">The <see langword="delegate" /> where log messages will be written.</param>
        public Context(IEnumerable<Assembly> assemblies, IDictionary<string, object> data, Action<string> logWriter)
        {
            Assemblies = new ReadOnlyCollection<Assembly>(assemblies.ToList());
            Data = data;
            LogWriter = logWriter;
        }


        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the assemblies used by KickStart.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public ReadOnlyCollection<Assembly> Assemblies { get; }

        /// <summary>
        /// Gets the data dictionary shared with all starter modules.
        /// </summary>
        /// <value>
        /// The data dictionary shared with all starter modules.
        /// </value>
        public IDictionary<string, object> Data { get; }
        
        /// <summary>
        /// Gets the <see langword="delegate" /> where log messages will be written.
        /// </summary>
        /// <value>
        /// The <see langword="delegate" /> where log messages will be written.
        /// </value>
        public Action<string> LogWriter { get; }


        /// <summary>
        /// Sets the global <see cref="P:KickStart.Kick.Container"/>.
        /// </summary>
        /// <param name="serviceProvider">The container adaptor to assign.</param>
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            WriteLog("Assign Kick service provider: {0}", serviceProvider);

            ServiceProvider = serviceProvider;
        }


        /// <summary>
        /// Gets the instances assignable from the specified generic type.
        /// </summary>
        /// <typeparam name="T">The Type to scan for</typeparam>
        /// <returns>An enumerable list of instances of type <typeparamref name="T"/>.</returns>
        public virtual IEnumerable<T> GetInstancesAssignableFrom<T>()
            where T : class
        {
            return Assemblies
                .SelectMany(GetTypesAssignableFrom<T>)
                .Select(CreateInstance)
                .OfType<T>()
                .ToList();
        }

        /// <summary>
        /// Gets the types assignable from type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to determine whether if it can be assigned.</typeparam>
        /// <param name="assembly">The assembly to search types.</param>
        /// <returns>An enumerable list of types the are assignable from <typeparamref name="T"/>.</returns>
        public virtual IEnumerable<Type> GetTypesAssignableFrom<T>(Assembly assembly)
        {
            WriteLog("Scan Start; Assembly: '{0}', Type: '{1}'", assembly.FullName, typeof(T));

            var watch = Stopwatch.StartNew();
            var types = assembly.GetTypesAssignableFrom<T>();
            watch.Stop();

            WriteLog("Scan Complete; Assembly: '{0}', Type: '{1}', Time: {2} ms", assembly.FullName, typeof(T), watch.ElapsedMilliseconds);

            return types;
        }


        /// <summary>
        /// Create an instance of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to create an instance of.</param>
        /// <returns>An instance of the specified <paramref name="type"/>.</returns>
        public virtual object CreateInstance(Type type)
        {
            WriteLog("Create Instance: {0}", type);

            object instance = null;

            // copy local for thread safty
            var provider = ServiceProvider;
            if (provider == null)
                return Activator.CreateInstance(type);

            try
            {
                WriteLog("Create '{0}' from service provider '{1}'", type);
                instance = provider.GetService(type);
            }
            catch (Exception ex)
            {
                WriteLog("Error creating '{0}' from service provider '{1}': {2}", type, provider, ex.Message);
            }

            return instance ?? Activator.CreateInstance(type);
        }


        /// <summary>
        /// Writes the formatted log message to the underlying log writer.
        /// </summary>
        /// <param name="messageFormatter">The message formatter.</param>
        public void WriteLog(Func<string> messageFormatter)
        {
            if (LogWriter == null)
                return;

            var message = messageFormatter();
            LogWriter(message);
        }

        /// <summary>
        /// Writes the formatted log message to the underlying log writer.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void WriteLog(string format, params object[] args)
        {
            WriteLog(() => string.Format(format, args));
        }
        
        /// <summary>
        /// Writes the message to the underlying log writer.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteLog(string message)
        {
            LogWriter?.Invoke(message);
        }

    }
}