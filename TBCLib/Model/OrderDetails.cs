public class OrderDetail
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }

    public Item Item { get; set; } 
}