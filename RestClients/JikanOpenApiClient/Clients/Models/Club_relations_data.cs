// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    public class Club_relations_data : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The anime property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Anime { get; set; }
#nullable restore
#else
        public List<Mal_url> Anime { get; set; }
#endif
        /// <summary>The characters property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Characters { get; set; }
#nullable restore
#else
        public List<Mal_url> Characters { get; set; }
#endif
        /// <summary>The manga property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Manga { get; set; }
#nullable restore
#else
        public List<Mal_url> Manga { get; set; }
#endif
        /// <summary>
        /// Instantiates a new club_relations_data and sets the default values.
        /// </summary>
        public Club_relations_data() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Club_relations_data CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Club_relations_data();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"anime", n => { Anime = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"characters", n => { Characters = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"manga", n => { Manga = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<Mal_url>("anime", Anime);
            writer.WriteCollectionOfObjectValues<Mal_url>("characters", Characters);
            writer.WriteCollectionOfObjectValues<Mal_url>("manga", Manga);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
