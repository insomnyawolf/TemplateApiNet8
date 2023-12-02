using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Externals
{
    [JsonPropertyName("tvrage")]
    public long Tvrage { get; set; }

    [JsonPropertyName("thetvdb")]
    public long Thetvdb { get; set; }

    [JsonPropertyName("imdb")]
    public string Imdb { get; set; }
}