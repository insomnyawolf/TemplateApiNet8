// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    /// <summary>
    /// Related resources
    /// </summary>
    public class Relation : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Related entries</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Entry { get; set; }
#nullable restore
#else
        public List<Mal_url> Entry { get; set; }
#endif
        /// <summary>Relation type</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RelationProp { get; set; }
#nullable restore
#else
        public string RelationProp { get; set; }
#endif
        /// <summary>
        /// Instantiates a new relation and sets the default values.
        /// </summary>
        public Relation() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Relation CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Relation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"entry", n => { Entry = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"relation", n => { RelationProp = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<Mal_url>("entry", Entry);
            writer.WriteStringValue("relation", RelationProp);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
