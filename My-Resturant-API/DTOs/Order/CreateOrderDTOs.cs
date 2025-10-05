using My_Resturant.Entities;

namespace My_Resturant.DTOs.Order
{
    public class CreateOrderDTOs
    {
        public int costumerId { get; set; }
        public string deliveryAdress { get; set; }
        public string? costumerNotes { get; set; }
        public double netPrice { get; set; }
        public string? rating { get; set; }
        public string? orderStatus { get; set; }
        public string? discountCode { get; set; }
        public List<OrderItemsDetialsDTOs>? cartItems { get; set; }
        public List<OrderMealsDetialsDTOs>? cartMeals { get; set; }

    }
}

