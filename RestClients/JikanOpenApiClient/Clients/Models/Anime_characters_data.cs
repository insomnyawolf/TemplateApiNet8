// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    public class Anime_characters_data : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Character details</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Anime_characters_data_character? Character { get; set; }
#nullable restore
#else
        public Anime_characters_data_character Character { get; set; }
#endif
        /// <summary>Character&apos;s Role</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Role { get; set; }
#nullable restore
#else
        public string Role { get; set; }
#endif
        /// <summary>The voice_actors property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Anime_characters_data_voice_actors>? VoiceActors { get; set; }
#nullable restore
#else
        public List<Anime_characters_data_voice_actors> VoiceActors { get; set; }
#endif
        /// <summary>
        /// Instantiates a new anime_characters_data and sets the default values.
        /// </summary>
        public Anime_characters_data() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Anime_characters_data CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Anime_characters_data();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"character", n => { Character = n.GetObjectValue<Anime_characters_data_character>(Anime_characters_data_character.CreateFromDiscriminatorValue); } },
                {"role", n => { Role = n.GetStringValue(); } },
                {"voice_actors", n => { VoiceActors = n.GetCollectionOfObjectValues<Anime_characters_data_voice_actors>(Anime_characters_data_voice_actors.CreateFromDiscriminatorValue)?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Anime_characters_data_character>("character", Character);
            writer.WriteStringValue("role", Role);
            writer.WriteCollectionOfObjectValues<Anime_characters_data_voice_actors>("voice_actors", VoiceActors);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
