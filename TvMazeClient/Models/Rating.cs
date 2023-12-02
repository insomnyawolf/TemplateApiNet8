using System.Text.Json.Serialization;

namespace TvMazeClient.Models;

public partial class Rating
{
    [JsonPropertyName("average")]
    public decimal Average { get; set; }
}