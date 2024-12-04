using System.Text.Json.Serialization;

namespace api_burguer_mania.Models;

public class Status {
    public int Id {get; set;}
    public required string Name {get; set;}
    [JsonIgnore]
    public ICollection<Order>? Orders {get; set;}
}