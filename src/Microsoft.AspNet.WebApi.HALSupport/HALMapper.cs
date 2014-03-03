namespace Microsoft.AspNet.WebApi.HALSupport
{
    /// <summary>
    /// Configuration class for configuring HAL links.
    /// </summary>
    public class HalMapper
    {
        /// <summary>
        /// The singleton
        /// </summary>
        private static readonly HalMapper Singleton = new HalMapper();

        /// <summary>
        /// Prevents a default instance of the <see cref="HalMapper"/> class from being created.
        /// </summary>
        private HalMapper()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static HalMapper Instance
        {
            get
            {
                return Singleton;
            }
        }
    }
}