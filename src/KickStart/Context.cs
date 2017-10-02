using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

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
        /// <param name="types">The types to scan by extensions.</param>
        /// <param name="data">The data dictionary shared with all starter modules.</param>
        /// <param name="logWriter">The <see langword="delegate" /> where log messages will be written.</param>
        public Context(IEnumerable<Type> types, IDictionary<string, object> data, Action<string> logWriter)
        {
            Types = new ReadOnlyCollection<Type>(types.ToList());
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
        /// Gets the types to scan by KickStart extensions.
        /// </summary>
        /// <value>
        /// The types to scan.
        /// </value>
        public ReadOnlyCollection<Type> Types { get; }

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
            var type = typeof(T);

            return GetTypesAssignableFrom(type)
                .Select(CreateInstance)
                .OfType<T>()
                .ToList();
        }

        /// <summary>
        /// Gets the types assignable from <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to determine whether if it can be assigned.</param>
        /// <returns>An enumerable list of types the are assignable from <paramref name="type"/>.</returns>
        public virtual IEnumerable<Type> GetTypesAssignableFrom(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return Types
                .Where(t =>
                {
                    var i = t.GetTypeInfo();
                    return i.IsPublic && !i.IsAbstract && typeInfo.IsAssignableFrom(i);
                });
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
                WriteLog("Create '{0}' from service provider '{1}'", type, provider);
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