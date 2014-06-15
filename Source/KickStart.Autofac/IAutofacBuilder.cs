using System;
using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAutofacBuilder
    {
        /// <summary>
        /// Sets Autofac build options.
        /// </summary>
        /// <param name="buildOptions">The build options.</param>
        /// <returns></returns>
        IAutofacBuilder Options(ContainerBuildOptions buildOptions);
        
        /// <summary>
        /// Sets the initialize builder <see langword="delegate"/>.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        IAutofacBuilder Builder(Action<ContainerBuilder> initializer);
        
        /// <summary>
        /// Sets the initialize container <see langword="delegate"/>.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        IAutofacBuilder Container(Action<IContainer> initializer);
    }
}