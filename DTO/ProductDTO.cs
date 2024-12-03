namespace api_burguer_mania.DTO;

public class ProductDTO {
    public required string Name {get; set;}
    public required string PathImage {get; set;}
    public required float Price {get; set;}
    public required string BaseDescription {get; set;}
    public required string FullDescription {get; set;}
    public required int CategoryId {get; set;}
}