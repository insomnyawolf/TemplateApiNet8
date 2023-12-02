using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Episode
{
    [JsonPropertyName("href")]
    public string Href { get; set; }
}