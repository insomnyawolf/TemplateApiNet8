// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    public class Anime_staff_data : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Person details</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Anime_staff_data_person? Person { get; set; }
#nullable restore
#else
        public Anime_staff_data_person Person { get; set; }
#endif
        /// <summary>Staff Positions</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Positions { get; set; }
#nullable restore
#else
        public List<string> Positions { get; set; }
#endif
        /// <summary>
        /// Instantiates a new anime_staff_data and sets the default values.
        /// </summary>
        public Anime_staff_data() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Anime_staff_data CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Anime_staff_data();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"person", n => { Person = n.GetObjectValue<Anime_staff_data_person>(Anime_staff_data_person.CreateFromDiscriminatorValue); } },
                {"positions", n => { Positions = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Anime_staff_data_person>("person", Person);
            writer.WriteCollectionOfPrimitiveValues<string>("positions", Positions);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
