// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    /// <summary>
    /// Club Resource
    /// </summary>
    public class Club : IAdditionalDataHolder, IParsable {
        /// <summary>Club access</summary>
        public Club_access? Access { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Club Category</summary>
        public Club_category? Category { get; set; }
        /// <summary>Date Created ISO8601</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Created { get; set; }
#nullable restore
#else
        public string Created { get; set; }
#endif
        /// <summary>The images property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Common_images? Images { get; set; }
#nullable restore
#else
        public Common_images Images { get; set; }
#endif
        /// <summary>MyAnimeList ID</summary>
        public int? MalId { get; set; }
        /// <summary>Number of club members</summary>
        public int? Members { get; set; }
        /// <summary>Club name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>Club URL</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; set; }
#nullable restore
#else
        public string Url { get; set; }
#endif
        /// <summary>
        /// Instantiates a new club and sets the default values.
        /// </summary>
        public Club() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Club CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Club();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"access", n => { Access = n.GetEnumValue<Club_access>(); } },
                {"category", n => { Category = n.GetEnumValue<Club_category>(); } },
                {"created", n => { Created = n.GetStringValue(); } },
                {"images", n => { Images = n.GetObjectValue<Common_images>(Common_images.CreateFromDiscriminatorValue); } },
                {"mal_id", n => { MalId = n.GetIntValue(); } },
                {"members", n => { Members = n.GetIntValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
                {"url", n => { Url = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<Club_access>("access", Access);
            writer.WriteEnumValue<Club_category>("category", Category);
            writer.WriteStringValue("created", Created);
            writer.WriteObjectValue<Common_images>("images", Images);
            writer.WriteIntValue("mal_id", MalId);
            writer.WriteIntValue("members", Members);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("url", Url);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
