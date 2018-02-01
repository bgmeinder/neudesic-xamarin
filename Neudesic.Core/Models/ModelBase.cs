namespace Neudesic.Core.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Entity base.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}