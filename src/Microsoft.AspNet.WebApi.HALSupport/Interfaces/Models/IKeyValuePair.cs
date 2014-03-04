namespace Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models
{
    /// <summary>
    /// A strongly typed key-value pair
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface IKeyValuePair<TKey, TValue> : IKey<TKey>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        TValue Value { get; set; }
    }

    /// <summary>
    /// A strongly typed key
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    public interface IKey<TKey>
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        TKey Key { get; set; }
    }
}