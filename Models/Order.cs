namespace api_burguer_mania.Models;

public class Order {
    public int Id {get; set;}
    public int StatusId {get; set;}
    public Status? Status {get; set;}
    public required float Value {get; set;}
    public string? Description {get; set;}
}