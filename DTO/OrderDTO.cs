using api_burguer_mania.DTO;

public class OrderDTO {
    public required int StatusId { get; set; }
    public required int UserId {get; set;}
    public required float Value { get; set; }
    public string? Description {get; set;} 
    public required ICollection<OrderItemDTO> Products {get; set;}
}
