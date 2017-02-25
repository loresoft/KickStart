using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector configuration builder
    /// </summary>
    public interface ISimpleInjectorBuilder
    {
        /// <summary>
        /// Sets the <see cref="Container"/> creator <see langword="delegate" />.
        /// </summary>
        /// <param name="creator">The <see cref="Container"/> creator.</param>
        /// <returns></returns>
        ISimpleInjectorBuilder Creator(Func<Container> creator);

        /// <summary>
        /// Sets the initialize container <see langword="delegate" />.  Register services under this <see langword="delegate" />.
        /// </summary>
        /// <param name="initializer">The initialize container <see langword="delegate"/>.</param>
        /// <returns></returns>
        ISimpleInjectorBuilder Initialize(Action<Container> initializer);

        /// <summary>
        /// Sets the container accessor <see langword="delegate" />.  Resolve services under this <see langword="delegate" />.
        /// </summary>
        /// <param name="accessor">The container accessor <see langword="delegate"/>.</param>
        /// <returns></returns>
        ISimpleInjectorBuilder Container(Action<Container> accessor);


        /// <summary>
        /// Verifies the Container with the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options"> Specifies how the container should verify its configuration.</param>
        /// <returns></returns>
        ISimpleInjectorBuilder Verify(VerificationOption? options);
    }
}