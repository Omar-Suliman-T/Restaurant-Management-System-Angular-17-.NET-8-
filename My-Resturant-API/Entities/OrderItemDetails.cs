namespace My_Resturant.Entities
{
    public class OrderItemDetails:MainEntity
    {
        public int orderId { get; set; }
        public int itemId { get; set; }
        public int quantity { get; set; }
    }
}
