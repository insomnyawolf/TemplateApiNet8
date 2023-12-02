using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Image
{
    [JsonPropertyName("medium")]
    public Uri Medium { get; set; }

    [JsonPropertyName("original")]
    public Uri Original { get; set; }
}