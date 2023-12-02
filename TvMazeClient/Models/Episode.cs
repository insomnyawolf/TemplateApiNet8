using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Episode
{
    [JsonPropertyName("href")]
    public Uri Href { get; set; }
}