// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    public class Character_full_anime : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The anime property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Anime_meta? Anime { get; set; }
#nullable restore
#else
        public Anime_meta Anime { get; set; }
#endif
        /// <summary>Character&apos;s Role</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Role { get; set; }
#nullable restore
#else
        public string Role { get; set; }
#endif
        /// <summary>
        /// Instantiates a new character_full_anime and sets the default values.
        /// </summary>
        public Character_full_anime() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Character_full_anime CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Character_full_anime();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"anime", n => { Anime = n.GetObjectValue<Anime_meta>(Anime_meta.CreateFromDiscriminatorValue); } },
                {"role", n => { Role = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Anime_meta>("anime", Anime);
            writer.WriteStringValue("role", Role);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
