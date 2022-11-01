using System.Text.Json.Serialization;

namespace PowerOff.Host.Models
{
    public class StreetForList
    {
        [JsonPropertyName("name")]
        public string? StreetName { get; set; }

        [JsonPropertyName("id")]
        public string? StreetId { get; set; }
    }
}
