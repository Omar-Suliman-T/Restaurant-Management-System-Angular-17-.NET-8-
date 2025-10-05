using My_Resturant.Entities;

namespace My_Resturant.DTOs.Order
{
    public class GetOrderDetailesDTOs:MainEntity
    {
        public int costumerId { get; set; }
        public string customerName { get; set; }
        public string orderStatus { get; set; }
        public string deliveryAdress { get; set; }
        public string? costumerNotes { get; set; }
        public string? rating { get; set; }
        public string? discountCode { get; set; }
        public double netPrice { get; set; }
        public List<OrderItemDetails>?OrderItemDetails {  get; set; }
        public List<OrderMealDetiales>?OrderMealDetiales { get; set; }
        
    }
}
