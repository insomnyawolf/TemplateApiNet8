// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace JikanRest.Models {
    /// <summary>
    /// Anime Resource
    /// </summary>
    public class Anime : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Date range</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Daterange? Aired { get; set; }
#nullable restore
#else
        public Daterange Aired { get; set; }
#endif
        /// <summary>Airing boolean</summary>
        public bool? Airing { get; set; }
        /// <summary>Whether the entry is pending approval on MAL or not</summary>
        public bool? Approved { get; set; }
        /// <summary>Background</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Background { get; set; }
#nullable restore
#else
        public string Background { get; set; }
#endif
        /// <summary>Broadcast Details</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public JikanRest.Models.Broadcast? Broadcast { get; set; }
#nullable restore
#else
        public JikanRest.Models.Broadcast Broadcast { get; set; }
#endif
        /// <summary>The demographics property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Demographics { get; set; }
#nullable restore
#else
        public List<Mal_url> Demographics { get; set; }
#endif
        /// <summary>Parsed raw duration</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Duration { get; set; }
#nullable restore
#else
        public string Duration { get; set; }
#endif
        /// <summary>Episode count</summary>
        public int? Episodes { get; set; }
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
        public Anime_images? Images { get; set; }
#nullable restore
#else
        public Anime_images Images { get; set; }
#endif
        /// <summary>The licensors property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Licensors { get; set; }
#nullable restore
#else
        public List<Mal_url> Licensors { get; set; }
#endif
        /// <summary>MyAnimeList ID</summary>
        public int? MalId { get; set; }
        /// <summary>Number of users who have added this entry to their list</summary>
        public int? Members { get; set; }
        /// <summary>Popularity</summary>
        public int? Popularity { get; set; }
        /// <summary>The producers property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Producers { get; set; }
#nullable restore
#else
        public List<Mal_url> Producers { get; set; }
#endif
        /// <summary>Ranking</summary>
        public int? Rank { get; set; }
        /// <summary>Anime audience rating</summary>
        public Anime_rating? Rating { get; set; }
        /// <summary>Score</summary>
        public float? Score { get; set; }
        /// <summary>Number of users</summary>
        public int? ScoredBy { get; set; }
        /// <summary>Season</summary>
        public Anime_season? Season { get; set; }
        /// <summary>Original Material/Source adapted from</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Source { get; set; }
#nullable restore
#else
        public string Source { get; set; }
#endif
        /// <summary>Airing status</summary>
        public Anime_status? Status { get; set; }
        /// <summary>The studios property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Mal_url>? Studios { get; set; }
#nullable restore
#else
        public List<Mal_url> Studios { get; set; }
#endif
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
        /// <summary>All titles</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<JikanRest.Models.Title>? Titles { get; set; }
#nullable restore
#else
        public List<JikanRest.Models.Title> Titles { get; set; }
#endif
        /// <summary>Other Titles</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? TitleSynonyms { get; set; }
#nullable restore
#else
        public List<string> TitleSynonyms { get; set; }
#endif
        /// <summary>Youtube Details</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Trailer_base? Trailer { get; set; }
#nullable restore
#else
        public Trailer_base Trailer { get; set; }
#endif
        /// <summary>Anime Type</summary>
        public Anime_type? Type { get; set; }
        /// <summary>MyAnimeList URL</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; set; }
#nullable restore
#else
        public string Url { get; set; }
#endif
        /// <summary>Year</summary>
        public int? Year { get; set; }
        /// <summary>
        /// Instantiates a new anime and sets the default values.
        /// </summary>
        public Anime() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Anime CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Anime();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"aired", n => { Aired = n.GetObjectValue<Daterange>(Daterange.CreateFromDiscriminatorValue); } },
                {"airing", n => { Airing = n.GetBoolValue(); } },
                {"approved", n => { Approved = n.GetBoolValue(); } },
                {"background", n => { Background = n.GetStringValue(); } },
                {"broadcast", n => { Broadcast = n.GetObjectValue<JikanRest.Models.Broadcast>(JikanRest.Models.Broadcast.CreateFromDiscriminatorValue); } },
                {"demographics", n => { Demographics = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"duration", n => { Duration = n.GetStringValue(); } },
                {"episodes", n => { Episodes = n.GetIntValue(); } },
                {"explicit_genres", n => { ExplicitGenres = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"favorites", n => { Favorites = n.GetIntValue(); } },
                {"genres", n => { Genres = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"images", n => { Images = n.GetObjectValue<Anime_images>(Anime_images.CreateFromDiscriminatorValue); } },
                {"licensors", n => { Licensors = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"mal_id", n => { MalId = n.GetIntValue(); } },
                {"members", n => { Members = n.GetIntValue(); } },
                {"popularity", n => { Popularity = n.GetIntValue(); } },
                {"producers", n => { Producers = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"rank", n => { Rank = n.GetIntValue(); } },
                {"rating", n => { Rating = n.GetEnumValue<Anime_rating>(); } },
                {"score", n => { Score = n.GetFloatValue(); } },
                {"scored_by", n => { ScoredBy = n.GetIntValue(); } },
                {"season", n => { Season = n.GetEnumValue<Anime_season>(); } },
                {"source", n => { Source = n.GetStringValue(); } },
                {"status", n => { Status = n.GetEnumValue<Anime_status>(); } },
                {"studios", n => { Studios = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"synopsis", n => { Synopsis = n.GetStringValue(); } },
                {"themes", n => { Themes = n.GetCollectionOfObjectValues<Mal_url>(Mal_url.CreateFromDiscriminatorValue)?.ToList(); } },
                {"title", n => { Title = n.GetStringValue(); } },
                {"title_english", n => { TitleEnglish = n.GetStringValue(); } },
                {"title_japanese", n => { TitleJapanese = n.GetStringValue(); } },
                {"title_synonyms", n => { TitleSynonyms = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"titles", n => { Titles = n.GetCollectionOfObjectValues<JikanRest.Models.Title>(JikanRest.Models.Title.CreateFromDiscriminatorValue)?.ToList(); } },
                {"trailer", n => { Trailer = n.GetObjectValue<Trailer_base>(Trailer_base.CreateFromDiscriminatorValue); } },
                {"type", n => { Type = n.GetEnumValue<Anime_type>(); } },
                {"url", n => { Url = n.GetStringValue(); } },
                {"year", n => { Year = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Daterange>("aired", Aired);
            writer.WriteBoolValue("airing", Airing);
            writer.WriteBoolValue("approved", Approved);
            writer.WriteStringValue("background", Background);
            writer.WriteObjectValue<JikanRest.Models.Broadcast>("broadcast", Broadcast);
            writer.WriteCollectionOfObjectValues<Mal_url>("demographics", Demographics);
            writer.WriteStringValue("duration", Duration);
            writer.WriteIntValue("episodes", Episodes);
            writer.WriteCollectionOfObjectValues<Mal_url>("explicit_genres", ExplicitGenres);
            writer.WriteIntValue("favorites", Favorites);
            writer.WriteCollectionOfObjectValues<Mal_url>("genres", Genres);
            writer.WriteObjectValue<Anime_images>("images", Images);
            writer.WriteCollectionOfObjectValues<Mal_url>("licensors", Licensors);
            writer.WriteIntValue("mal_id", MalId);
            writer.WriteIntValue("members", Members);
            writer.WriteIntValue("popularity", Popularity);
            writer.WriteCollectionOfObjectValues<Mal_url>("producers", Producers);
            writer.WriteIntValue("rank", Rank);
            writer.WriteEnumValue<Anime_rating>("rating", Rating);
            writer.WriteFloatValue("score", Score);
            writer.WriteIntValue("scored_by", ScoredBy);
            writer.WriteEnumValue<Anime_season>("season", Season);
            writer.WriteStringValue("source", Source);
            writer.WriteEnumValue<Anime_status>("status", Status);
            writer.WriteCollectionOfObjectValues<Mal_url>("studios", Studios);
            writer.WriteStringValue("synopsis", Synopsis);
            writer.WriteCollectionOfObjectValues<Mal_url>("themes", Themes);
            writer.WriteStringValue("title", Title);
            writer.WriteStringValue("title_english", TitleEnglish);
            writer.WriteStringValue("title_japanese", TitleJapanese);
            writer.WriteCollectionOfObjectValues<JikanRest.Models.Title>("titles", Titles);
            writer.WriteCollectionOfPrimitiveValues<string>("title_synonyms", TitleSynonyms);
            writer.WriteObjectValue<Trailer_base>("trailer", Trailer);
            writer.WriteEnumValue<Anime_type>("type", Type);
            writer.WriteStringValue("url", Url);
            writer.WriteIntValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
