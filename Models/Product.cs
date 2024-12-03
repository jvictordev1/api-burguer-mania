using System.Text.Json.Serialization;

namespace api_burguer_mania.Models;

public class Product {
    public int Id {get; set;}
    public required string Name {get; set;}
    public required string PathImage {get; set;}
    public required float Price {get; set;}
    public required string BaseDescription {get; set;}
    public required string FullDescription {get; set;}
    public int CategoryId {get; set;}
    [JsonIgnore]
    public Category? Category {get; set;}
    
    [JsonIgnore]
    public ICollection<Order>? Order {get; set;}
}