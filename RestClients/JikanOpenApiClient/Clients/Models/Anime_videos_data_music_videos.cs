// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    public class Anime_videos_data_music_videos : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The meta property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Anime_videos_data_music_videos_meta? Meta { get; set; }
#nullable restore
#else
        public Anime_videos_data_music_videos_meta Meta { get; set; }
#endif
        /// <summary>Title</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Title { get; set; }
#nullable restore
#else
        public string Title { get; set; }
#endif
        /// <summary>Youtube Details</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Trailer? Video { get; set; }
#nullable restore
#else
        public Trailer Video { get; set; }
#endif
        /// <summary>
        /// Instantiates a new anime_videos_data_music_videos and sets the default values.
        /// </summary>
        public Anime_videos_data_music_videos() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Anime_videos_data_music_videos CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Anime_videos_data_music_videos();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"meta", n => { Meta = n.GetObjectValue<Anime_videos_data_music_videos_meta>(Anime_videos_data_music_videos_meta.CreateFromDiscriminatorValue); } },
                {"title", n => { Title = n.GetStringValue(); } },
                {"video", n => { Video = n.GetObjectValue<Trailer>(Trailer.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Anime_videos_data_music_videos_meta>("meta", Meta);
            writer.WriteStringValue("title", Title);
            writer.WriteObjectValue<Trailer>("video", Video);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}