// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    /// <summary>
    /// User reaction count on the review
    /// </summary>
    public class Manga_review_reactions : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Confusing reaction count</summary>
        public int? Confusing { get; set; }
        /// <summary>Creative reaction count</summary>
        public int? Creative { get; set; }
        /// <summary>Funny reaction count</summary>
        public int? Funny { get; set; }
        /// <summary>Informative reaction count</summary>
        public int? Informative { get; set; }
        /// <summary>Love it reaction count</summary>
        public int? LoveIt { get; set; }
        /// <summary>Nice reaction count</summary>
        public int? Nice { get; set; }
        /// <summary>Overall reaction count</summary>
        public int? Overall { get; set; }
        /// <summary>Well written reaction count</summary>
        public int? WellWritten { get; set; }
        /// <summary>
        /// Instantiates a new manga_review_reactions and sets the default values.
        /// </summary>
        public Manga_review_reactions() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Manga_review_reactions CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Manga_review_reactions();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"confusing", n => { Confusing = n.GetIntValue(); } },
                {"creative", n => { Creative = n.GetIntValue(); } },
                {"funny", n => { Funny = n.GetIntValue(); } },
                {"informative", n => { Informative = n.GetIntValue(); } },
                {"love_it", n => { LoveIt = n.GetIntValue(); } },
                {"nice", n => { Nice = n.GetIntValue(); } },
                {"overall", n => { Overall = n.GetIntValue(); } },
                {"well_written", n => { WellWritten = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("confusing", Confusing);
            writer.WriteIntValue("creative", Creative);
            writer.WriteIntValue("funny", Funny);
            writer.WriteIntValue("informative", Informative);
            writer.WriteIntValue("love_it", LoveIt);
            writer.WriteIntValue("nice", Nice);
            writer.WriteIntValue("overall", Overall);
            writer.WriteIntValue("well_written", WellWritten);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
