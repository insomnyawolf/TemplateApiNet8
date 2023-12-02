using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Schedule
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("days")]
    public List<string> Days { get; set; }
}