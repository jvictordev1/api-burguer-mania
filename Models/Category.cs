namespace api_burguer_mania.Models;

public class Category {
    public int Id {get; set;}
    public required string Name {get; set;}
    public required string Description {get; set;}
    public required string PathImage {get; set;}
    public ICollection<Product>? Products {get; set;}
}