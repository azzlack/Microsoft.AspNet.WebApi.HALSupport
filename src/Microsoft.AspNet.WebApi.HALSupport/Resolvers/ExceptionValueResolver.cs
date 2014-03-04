namespace Microsoft.AspNet.WebApi.HALSupport.Resolvers
{
    using System;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    /// <summary>
    /// A value resolver with an exception.
    /// </summary>
    /// <typeparam name="T">The value resolver in question.</typeparam>
    public class ExceptionValueResolver<T> : Exception, IValueResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionValueResolver{T}"/> class.
        /// </summary>
        public ExceptionValueResolver() 
            : base(string.Format("An exception occured when trying to create instance of '{0}'", typeof(T)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionValueResolver{T}" /> class.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public ExceptionValueResolver(Exception ex)
            : base(string.Format("An exception occured when trying to create instance of '{0}'", typeof(T)), ex)
        {
        }

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value</returns>
        public object Resolve(object sender)
        {
            return null;
        }

        /// <summary>
        /// Resolves the specified sender.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="sender">The sender.</param>
        /// <returns>The resolved value.</returns>
        public TResult Resolve<TResult>(object sender)
        {
            return default(TResult);
        }

        /// <summary>
        /// Resolves the specified sender.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Resolve<TResult>(object sender, out TResult value)
        {
            value = default(TResult);

            return true;
        }
    }
}