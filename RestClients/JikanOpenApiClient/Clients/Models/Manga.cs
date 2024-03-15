// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    /// <summary>
    /// Manga Resource
    /// </summary>
    public class Manga : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Whether the entry is pending approval on MAL or not</summary>
        public bool? Approved { get; set; }
        /// <summary>The authors property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Authors { get; set; }
#nullable restore
#else
        public List<Mal_url> Authors { get; set; }
#endif
        /// <summary>Background</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Background { get; set; }
#nullable restore
#else
        public string Background { get; set; }
#endif
        /// <summary>Chapter count</summary>
        public int? Chapters { get; set; }
        /// <summary>The demographics property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Demographics { get; set; }
#nullable restore
#else
        public List<Mal_url> Demographics { get; set; }
#endif
        /// <summary>The explicit_genres property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? ExplicitGenres { get; set; }
#nullable restore
#else
        public List<Mal_url> ExplicitGenres { get; set; }
#endif
        /// <summary>Number of users who have favorited this entry</summary>
        public int? Favorites { get; set; }
        /// <summary>The genres property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Genres { get; set; }
#nullable restore
#else
        public List<Mal_url> Genres { get; set; }
#endif
        /// <summary>The images property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Manga_images? Images { get; set; }
#nullable restore
#else
        public Manga_images Images { get; set; }
#endif
        /// <summary>MyAnimeList ID</summary>
        public int? MalId { get; set; }
        /// <summary>Number of users who have added this entry to their list</summary>
        public int? Members { get; set; }
        /// <summary>Popularity</summary>
        public int? Popularity { get; set; }
        /// <summary>Date range</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Daterange? Published { get; set; }
#nullable restore
#else
        public Daterange Published { get; set; }
#endif
        /// <summary>Publishing boolean</summary>
        public bool? Publishing { get; set; }
        /// <summary>Ranking</summary>
        public int? Rank { get; set; }
        /// <summary>Score</summary>
        public float? Score { get; set; }
        /// <summary>Number of users</summary>
        public int? ScoredBy { get; set; }
        /// <summary>The serializations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Serializations { get; set; }
#nullable restore
#else
        public List<Mal_url> Serializations { get; set; }
#endif
        /// <summary>Publishing status</summary>
        public Manga_status? Status { get; set; }
        /// <summary>Synopsis</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Synopsis { get; set; }
#nullable restore
#else
        public string Synopsis { get; set; }
#endif
        /// <summary>The themes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Themes { get; set; }
#nullable restore
#else
        public List<Mal_url> Themes { get; set; }
#endif
        /// <summary>Title</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Title { get; set; }
#nullable restore
#else
        public string Title { get; set; }
#endif
        /// <summary>English Title</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TitleEnglish { get; set; }
#nullable restore
#else
        public string TitleEnglish { get; set; }
#endif
        /// <summary>Japanese Title</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TitleJapanese { get; set; }
#nullable restore
#else
        public string TitleJapanese { get; set; }
#endif
        /// <summary>All Titles</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<JikanRest.Models.Title>? Titles { get; set; }
#nullable restore
#else
        public List<JikanRest.Models.Title> Titles { get; set; }
#endif
        /// <summary>Manga Type</summary>
        public Manga_type? Type { get; set; }
        /// <summary>MyAnimeList URL</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; set; }
#nullable restore
#else
        public string Url { get; set; }
#endif
        /// <summary>Volume count</summary>
        public int? Volumes { get; set; }
        /// <summary>
        /// Instantiates a new manga and sets the default values.
        /// </summary>
        public Manga() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Manga CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Manga();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"approved", n => { Approved = n.GetBoolValue(); } },
                {"authors", n => { Authors = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"background", n => { Background = n.GetStringValue(); } },
                {"chapters", n => { Chapters = n.GetIntValue(); } },
                {"demographics", n => { Demographics = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"explicit_genres", n => { ExplicitGenres = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"favorites", n => { Favorites = n.GetIntValue(); } },
                {"genres", n => { Genres = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"images", n => { Images = n.GetObjectValue<Manga_images>(Manga_images.CreateFromDiscriminatorValue); } },
                {"mal_id", n => { MalId = n.GetIntValue(); } },
                {"members", n => { Members = n.GetIntValue(); } },
                {"popularity", n => { Popularity = n.GetIntValue(); } },
                {"published", n => { Published = n.GetObjectValue<Daterange>(Daterange.CreateFromDiscriminatorValue); } },
                {"publishing", n => { Publishing = n.GetBoolValue(); } },
                {"rank", n => { Rank = n.GetIntValue(); } },
                {"score", n => { Score = n.GetFloatValue(); } },
                {"scored_by", n => { ScoredBy = n.GetIntValue(); } },
                {"serializations", n => { Serializations = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"status", n => { Status = n.GetEnumValue<Manga_status>(); } },
                {"synopsis", n => { Synopsis = n.GetStringValue(); } },
                {"themes", n => { Themes = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"title", n => { Title = n.GetStringValue(); } },
                {"title_english", n => { TitleEnglish = n.GetStringValue(); } },
                {"title_japanese", n => { TitleJapanese = n.GetStringValue(); } },
                {"titles", n => { Titles = n.GetCollectionOfObjectValues<JikanRest.Models.Title>(JikanRest.Models.Title.CreateFromDiscriminatorValue)?.ToList(); } },
                {"type", n => { Type = n.GetEnumValue<Manga_type>(); } },
                {"url", n => { Url = n.GetStringValue(); } },
                {"volumes", n => { Volumes = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("approved", Approved);
            writer.WriteCollectionOfObjectValues<Mal_url>("authors", Authors);
            writer.WriteStringValue("background", Background);
            writer.WriteIntValue("chapters", Chapters);
            writer.WriteCollectionOfObjectValues<Mal_url>("demographics", Demographics);
            writer.WriteCollectionOfObjectValues<Mal_url>("explicit_genres", ExplicitGenres);
            writer.WriteIntValue("favorites", Favorites);
            writer.WriteCollectionOfObjectValues<Mal_url>("genres", Genres);
            writer.WriteObjectValue<Manga_images>("images", Images);
            writer.WriteIntValue("mal_id", MalId);
            writer.WriteIntValue("members", Members);
            writer.WriteIntValue("popularity", Popularity);
            writer.WriteObjectValue<Daterange>("published", Published);
            writer.WriteBoolValue("publishing", Publishing);
            writer.WriteIntValue("rank", Rank);
            writer.WriteFloatValue("score", Score);
            writer.WriteIntValue("scored_by", ScoredBy);
            writer.WriteCollectionOfObjectValues<Mal_url>("serializations", Serializations);
            writer.WriteEnumValue<Manga_status>("status", Status);
            writer.WriteStringValue("synopsis", Synopsis);
            writer.WriteCollectionOfObjectValues<Mal_url>("themes", Themes);
            writer.WriteStringValue("title", Title);
            writer.WriteStringValue("title_english", TitleEnglish);
            writer.WriteStringValue("title_japanese", TitleJapanese);
            writer.WriteCollectionOfObjectValues<JikanRest.Models.Title>("titles", Titles);
            writer.WriteEnumValue<Manga_type>("type", Type);
            writer.WriteStringValue("url", Url);
            writer.WriteIntValue("volumes", Volumes);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
