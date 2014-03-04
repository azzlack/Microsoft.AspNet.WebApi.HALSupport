namespace Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models
{
    /// <summary>
    /// Interface for property mapping.
    /// </summary>
    public interface IPropertyMapping
    {
        /// <summary>
        /// Uses the specified value when mapping the property.
        /// </summary>
        /// <param name="value">The value.</param>
        void Use(object value);

        /// <summary>
        /// Uses the speficied value resolver when mapping the property.
        /// </summary>
        /// <typeparam name="T">The value resolver type</typeparam>
        /// <returns>The value resolver mapping configuration.</returns>
        IValueResolverMapping<T> Use<T>() where T : IValueResolver;
    }
}