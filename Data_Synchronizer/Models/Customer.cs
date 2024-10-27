using System.Text.Json.Serialization;

namespace Data_Synchronizer.Models;

public class Customer
{
    [JsonPropertyName("customerId")]
    public int CustomerId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("phone")]
    public required string Phone { get; set; }

}
