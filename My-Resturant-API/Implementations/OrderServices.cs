using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.Controllers;
using My_Resturant.DTOs.Order;
using My_Resturant.DTOs.Person;
using My_Resturant.Entities;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class OrderServices : IOrderServices
    {
        private readonly RestDbContext _context;
        public OrderServices(RestDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetOrderDetailesDTOs>> GetAllOrders()
        {
            var orders = await (
                from order in _context.Orders
                join status in _context.LookupItems
                    on order.orderStatus equals status.id
                join customer in _context.People 
                    on order.costumerId equals customer.id  
                join ratingLookup in _context.LookupItems
                    on order.rating equals ratingLookup.id into ratingGroup
                        from ratingLookup in ratingGroup.DefaultIfEmpty()  // LEFT JOIN
                select new GetOrderDetailesDTOs
                {
                    id = order.id,
                    costumerId = order.costumerId,
                    customerName=customer.firstName+" "+customer.lastName,
                    costumerNotes = order.costumerNotes,
                    deliveryAdress = order.deliveryAdress,
                    netPrice = order.netPrice,
                    isActive = order.isActive,

                    orderStatus = status.name,
                    rating = order.rating == null ? null : ratingLookup.name,
                    discountCode = order.discountCode,
                    creationDate = order.creationDate,
                    modificationDate = order.modificationDate,
                }
            ).ToListAsync();
            

            return orders;
        }



        public async Task<GetOrderDetailesDTOs> GetOrderDetiales(int orderId)
        {   
            var theOrder =await _context.Orders.FirstOrDefaultAsync(order => order.id == orderId);
            var status =await _context.LookupItems.FirstOrDefaultAsync(li => li.id == theOrder.orderStatus);
            var rating =await _context.LookupItems.FirstOrDefaultAsync(li => li.id == theOrder.rating);
            var theOrderItemDetailes = _context.OrderItemDetails.Where(item => item.orderId == orderId).ToList();
            var theOrderMealDetailes = _context.OrderMealDetails.Where(item => item.OrderId == orderId).ToList();
            if (theOrder == null)
            {
                throw new Exception($"couldn't find an order with the id:{orderId}");
            }
            else
            {
                GetOrderDetailesDTOs getOrderDetailesDTOs = new GetOrderDetailesDTOs
                {
                    costumerId = theOrder.costumerId,
                    costumerNotes = theOrder.costumerNotes,
                    deliveryAdress = theOrder.deliveryAdress,
                    orderStatus =status.name,
                    rating = rating.name,
                    netPrice = theOrder.netPrice,
                    discountCode = theOrder.discountCode,
                    OrderItemDetails = theOrderItemDetailes,
                    OrderMealDetiales = theOrderMealDetailes,
                };
                return getOrderDetailesDTOs;

            }
        }
        public async Task<GetOrderDetailesDTOs> GetLastOrderDetiales()
        {
            var lastOrder = await _context.Orders
                .OrderByDescending(o => o.id).FirstOrDefaultAsync();
            if (lastOrder == null)
            {
                throw new Exception("Couldn't find the order");
            }
            var theOrderItemDetailes = _context.OrderItemDetails.Where(item => item.orderId == lastOrder.id).ToList();
            var theOrderMealDetailes = _context.OrderMealDetails.Where(item => item.OrderId == lastOrder.id).ToList();
            var status = await _context.LookupItems.FirstOrDefaultAsync(li => li.id == lastOrder.orderStatus);
            var rating = await _context.LookupItems.FirstOrDefaultAsync(li => li.id == lastOrder.rating);

            GetOrderDetailesDTOs theLastOrder = new GetOrderDetailesDTOs
            {
                id = lastOrder.id,
                creationDate = lastOrder.creationDate,
                costumerId = lastOrder.costumerId,
                costumerNotes = lastOrder.costumerNotes,
                deliveryAdress = lastOrder.deliveryAdress,
                orderStatus = status.name,
                rating = rating==null?null:rating.name,
                netPrice = lastOrder.netPrice,
                discountCode = lastOrder.discountCode,
                OrderItemDetails = theOrderItemDetailes,
                OrderMealDetiales = theOrderMealDetailes,
            };

            return theLastOrder;
        }
        public async Task UpdateOrder(int orderId, UpdateOrderDTOs updateOrderDTOs)
        {
            var rating = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == updateOrderDTOs.rating);

            var Order = await _context.Orders.FirstOrDefaultAsync(order => order.id == orderId);
            if (Order != null)
            {
                if (updateOrderDTOs.rating != null)
                {
                    Order.rating = rating.id;
                }
                if (updateOrderDTOs.customerNotes != null)
                {
                    Order.costumerNotes = updateOrderDTOs.customerNotes;
                }
                if (updateOrderDTOs.deliveryAdress != null)
                {
                    Order.deliveryAdress = updateOrderDTOs.deliveryAdress;
                }
                await _context.SaveChangesAsync();
            }
            
            else
            {
                throw new Exception("no order found");
            }
        }
        public async Task CreateOrder(CreateOrderDTOs createOrderDTOs)
        {
            var status = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == createOrderDTOs.orderStatus);
            var rating = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == createOrderDTOs.rating);


            Order order = new Order
            {
                costumerId = createOrderDTOs.costumerId,
                deliveryAdress = createOrderDTOs.deliveryAdress,
                orderStatus = status.id,
                costumerNotes = createOrderDTOs.costumerNotes,
                netPrice = createOrderDTOs.netPrice,
                rating = rating==null?null:rating.id,
                discountCode=createOrderDTOs.discountCode,
                isActive = true
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            var lastOrderId= _context.Orders.OrderByDescending(order => order.id).FirstOrDefault().id;

            if (createOrderDTOs.cartItems != null)
            {
                var itemDetailsList = createOrderDTOs.cartItems.Select(item => new OrderItemDetails
                {
                    orderId = lastOrderId,
                    itemId = item.id,
                    quantity = item.quantity
                }).ToList();

                await _context.OrderItemDetails.AddRangeAsync(itemDetailsList);
                await _context.SaveChangesAsync();
            }

            if (createOrderDTOs.cartMeals != null)
            {
                var mealDetailsList = createOrderDTOs.cartMeals.Select(meal => new OrderMealDetiales
                {
                    OrderId = lastOrderId,
                    MealId = meal.mealId,
                    quantity = meal.quantity
                }).ToList();

                await _context.OrderMealDetails.AddRangeAsync(mealDetailsList);
                await _context.SaveChangesAsync();
            }

        }
        public async Task CancelOrder(int orderId)
        {
           var theOrder=await _context.Orders.FirstOrDefaultAsync(order => order.id == orderId);
            _context.Orders.Remove(theOrder);
            await _context.SaveChangesAsync();
            var theOrderItemDeitails = await _context.OrderItemDetails.Where(order => order.orderId == orderId).ToListAsync();
            if (theOrderItemDeitails != null)
            {
                _context.OrderItemDetails.RemoveRange(theOrderItemDeitails);
                await _context.SaveChangesAsync();
            }
            var theOrderMealsDeitails = await _context.OrderMealDetails.Where(order => order.OrderId == orderId).ToListAsync();
            if(theOrderMealsDeitails != null)
            {
                _context.OrderMealDetails.RemoveRange(theOrderMealsDeitails);
                await _context.SaveChangesAsync();
            }

        }
        
    }
}
