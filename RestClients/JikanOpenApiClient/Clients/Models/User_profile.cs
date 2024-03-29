// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    public class User_profile : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Birthday Date ISO8601</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Birthday { get; set; }
#nullable restore
#else
        public string Birthday { get; set; }
#endif
        /// <summary>User Gender</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Gender { get; set; }
#nullable restore
#else
        public string Gender { get; set; }
#endif
        /// <summary>The images property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public User_images? Images { get; set; }
#nullable restore
#else
        public User_images Images { get; set; }
#endif
        /// <summary>Joined Date ISO8601</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Joined { get; set; }
#nullable restore
#else
        public string Joined { get; set; }
#endif
        /// <summary>Last Online Date ISO8601</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastOnline { get; set; }
#nullable restore
#else
        public string LastOnline { get; set; }
#endif
        /// <summary>Location</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Location { get; set; }
#nullable restore
#else
        public string Location { get; set; }
#endif
        /// <summary>MyAnimeList ID</summary>
        public int? MalId { get; set; }
        /// <summary>MyAnimeList URL</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; set; }
#nullable restore
#else
        public string Url { get; set; }
#endif
        /// <summary>MyAnimeList Username</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Username { get; set; }
#nullable restore
#else
        public string Username { get; set; }
#endif
        /// <summary>
        /// Instantiates a new user_profile and sets the default values.
        /// </summary>
        public User_profile() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static User_profile CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new User_profile();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"birthday", n => { Birthday = n.GetStringValue(); } },
                {"gender", n => { Gender = n.GetStringValue(); } },
                {"images", n => { Images = n.GetObjectValue<User_images>(User_images.CreateFromDiscriminatorValue); } },
                {"joined", n => { Joined = n.GetStringValue(); } },
                {"last_online", n => { LastOnline = n.GetStringValue(); } },
                {"location", n => { Location = n.GetStringValue(); } },
                {"mal_id", n => { MalId = n.GetIntValue(); } },
                {"url", n => { Url = n.GetStringValue(); } },
                {"username", n => { Username = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("birthday", Birthday);
            writer.WriteStringValue("gender", Gender);
            writer.WriteObjectValue<User_images>("images", Images);
            writer.WriteStringValue("joined", Joined);
            writer.WriteStringValue("last_online", LastOnline);
            writer.WriteStringValue("location", Location);
            writer.WriteIntValue("mal_id", MalId);
            writer.WriteStringValue("url", Url);
            writer.WriteStringValue("username", Username);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
