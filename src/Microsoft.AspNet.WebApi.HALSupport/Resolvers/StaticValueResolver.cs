namespace Microsoft.AspNet.WebApi.HALSupport.Resolvers
{
    using System;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    /// <summary>
    /// Value resolver for static values.
    /// </summary>
    public class StaticValueResolver : IValueResolver
    {
        /// <summary>
        /// The value
        /// </summary>
        private readonly object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticValueResolver"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StaticValueResolver(object value)
        {
            this.value = value;
        }

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns>Result, typically built from the source resolution result</returns>
        public object Resolve(object sender)
        {
            return this.value;
        }

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <typeparam name="T">The type to resolve to</typeparam>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value</returns>
        public T Resolve<T>(object sender)
        {
            if (this.value == null)
            {
                return default(T);
            }

            if (this.value is T)
            {
                return (T)this.value;
            }
            
            if (typeof(T).IsAssignableFrom(typeof(Uri)))
            {
                if (this.value is string && Uri.IsWellFormedUriString(this.value.ToString(), UriKind.RelativeOrAbsolute))
                {
                    var v = new Uri(this.value.ToString(), UriKind.RelativeOrAbsolute);

                    return (T)Convert.ChangeType(v, typeof(T));
                }
            }

            return (T)Convert.ChangeType(this.value, typeof(T));
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
            catch
            {
                value = default(T);

                return false;
            }
        }
    }
}