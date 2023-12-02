using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Show
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("runtime")]
    public long Runtime { get; set; }

    [JsonPropertyName("averageRuntime")]
    public long AverageRuntime { get; set; }

    [JsonPropertyName("premiered")]
    public string Premiered { get; set; }

    [JsonPropertyName("ended")]
    public string Ended { get; set; }

    [JsonPropertyName("officialSite")]
    public Uri OfficialSite { get; set; }

    [JsonPropertyName("schedule")]
    public Schedule Schedule { get; set; }

    [JsonPropertyName("rating")]
    public Rating Rating { get; set; }

    [JsonPropertyName("weight")]
    public long Weight { get; set; }

    [JsonPropertyName("network")]
    public Network Network { get; set; }

    [JsonPropertyName("webChannel")]
    public dynamic WebChannel { get; set; }

    [JsonPropertyName("dvdCountry")]
    public dynamic DvdCountry { get; set; }

    [JsonPropertyName("externals")]
    public Externals Externals { get; set; }

    [JsonPropertyName("image")]
    public Image Image { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("updated")]
    public long Updated { get; set; }

    [JsonPropertyName("_links")]
    public Links Links { get; set; }
}
