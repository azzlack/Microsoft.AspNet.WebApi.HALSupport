namespace Microsoft.AspNet.WebApi.HALSupport.Resolvers
{
    using System;
    using System.Web;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    public class HttpContextResolver : IValueResolver
    {
        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value</returns>
        public object Resolve(object sender)
        {
            if (HttpContext.Current != null)
            {
                return new Uri(HttpContext.Current.Request.RawUrl, UriKind.RelativeOrAbsolute);
            }

            return new Uri("/", UriKind.Relative);
        }

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <typeparam name="T">The type to resolve to</typeparam>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value</returns>
        public T Resolve<T>(object sender)
        {
            return (T)this.Resolve(sender);
        }

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <typeparam name="T">The type to resolve to</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the resolving was successful, <c>false</c> otherwise.</returns>
        public bool Resolve<T>(object sender, out T value)
        {
            try
            {
                value = this.Resolve<T>(sender);

                return true;
            }
            catch (Exception ex)
            {
                value = default(T);

                return false;
            }
        }
    }
}