using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Links
{
    [JsonPropertyName("self")]
    public Episode Self { get; set; }

    [JsonPropertyName("previousepisode")]
    public Episode Previousepisode { get; set; }
}