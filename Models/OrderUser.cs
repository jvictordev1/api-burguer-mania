namespace api_burguer_mania.Models;

public class OrderUser {
    public int Id {get; set;}
    public required int UserId {get; set;}
    public User? User {get; set;}
    public required int OrderId {get; set;}
    public Order? Order {get; set;}
}