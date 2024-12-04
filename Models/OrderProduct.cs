namespace api_burguer_mania.Models;

public class OrderProduct {
    public int Id { get; set; }
    public required int OrderId { get; set; }
    public Order? Order { get; set; }
    public required int ProductId { get; set; }
    public Product? Product { get; set; }
}
