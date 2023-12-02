using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Image
{
    [JsonPropertyName("medium")]
    public string Medium { get; set; }

    [JsonPropertyName("original")]
    public string Original { get; set; }
}