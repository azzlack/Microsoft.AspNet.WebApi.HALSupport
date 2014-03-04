namespace Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models
{
    using System;

    /// <summary>
    /// Interface for configuring a value resolver.
    /// </summary>
    /// <typeparam name="T">The value resolver type</typeparam>
    public interface IValueResolverMapping<T> where T : IValueResolver
    {
        /// <summary>
        /// Sets the constructor to use for this value resolver.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        void ConstructedBy(Func<T> constructor);
    }
}