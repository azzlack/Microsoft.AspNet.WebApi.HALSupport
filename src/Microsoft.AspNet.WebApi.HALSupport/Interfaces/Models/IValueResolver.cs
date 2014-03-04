namespace Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models
{
    /// <summary>
    /// Extension point to provide custom resolution for a value.
    /// </summary>
    public interface IValueResolver
    {
        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value</returns>
        object Resolve(object sender);

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <typeparam name="T">The type to resolve to</typeparam>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value</returns>
        T Resolve<T>(object sender);

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <typeparam name="T">The type to resolve to</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the resolving was successful, <c>false</c> otherwise.</returns>
        bool Resolve<T>(object sender, out T value);
    }
}