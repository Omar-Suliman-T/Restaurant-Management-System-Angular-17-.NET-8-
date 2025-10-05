using My_Resturant.DTOs.Order;
using My_Resturant.Entities;

namespace My_Resturant.Interfaces
{
    public interface IOrderServices
    {
        Task<GetOrderDetailesDTOs> GetOrderDetiales(int orderId);
        Task<GetOrderDetailesDTOs> GetLastOrderDetiales();
        Task<List<GetOrderDetailesDTOs>> GetAllOrders();
        Task CreateOrder(CreateOrderDTOs createOrderDTOs);
        Task UpdateOrder(int orderId, UpdateOrderDTOs updateOrderDTOs);
        Task CancelOrder(int orderId);
    }
}
