using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Network
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public Country Country { get; set; }

    [JsonPropertyName("officialSite")]
    public Uri OfficialSite { get; set; }
}