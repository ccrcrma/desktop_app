using System.Text.Json.Serialization;

namespace Data_Synchronizer.Models;

public class Location
{
    [JsonPropertyName("locationId")]
    public int LocationId { get; set; }
    [JsonPropertyName("customerId")]
    public long CustomerId { get; set; }

    [JsonPropertyName("address")]
    public required string Address { get; set; }

}
