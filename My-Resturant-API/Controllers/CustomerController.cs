using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Resturant.DTOs.Order;
using My_Resturant.DTOs.Person;
using My_Resturant.DTOs.Resevation;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Implementations;
using My_Resturant.Interfaces;
using System.Runtime.CompilerServices;

namespace My_Resturant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICategoryServises _categoryServises;
        private readonly IItemServices _itemServices;
        private readonly IMealServices _mealServices;
        private readonly IOrderServices _orderServices;
        private readonly IAuthServices _authServices;
        private readonly IMealDetialsServices _mealDetialsServices;
        private readonly IPersonServices _personServices;
        private readonly IReservationServices _reservationServices;
        private readonly ICodeServices _codeServices;
        private readonly TokenHelper _tokenHelper;
        public CustomerController(ICategoryServises categoryServises, IItemServices itemServices, IMealServices mealServices,
            IOrderServices orderServices,IAuthServices authServices, TokenHelper tokenHelper,IMealDetialsServices mealDetialesServices,
            IPersonServices personServices,IReservationServices reservationServices,ICodeServices codeServices)
        {
            _categoryServises = categoryServises;
            _itemServices = itemServices;
            _mealServices = mealServices;
            _orderServices = orderServices;
            _authServices = authServices;
            _tokenHelper = tokenHelper;
            _mealDetialsServices = mealDetialesServices;
            _personServices = personServices;
            _reservationServices = reservationServices;
            _codeServices = codeServices;
        }
    //==========================================================================================================================================
    //Categories:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCategories([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _categoryServises.GetCategories();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
    //==========================================================================================================================================
    //Items:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetItems([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _itemServices.GetAllItems();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMostPopularItems([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _itemServices.GetMostPopularItems();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        //==========================================================================================================================================
        //Meals:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMeals([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _mealServices.GetAllMeals();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }

        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetSpecificMealDetiales([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _mealServices.GetSpecificMealDetiales(id);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetMealItems([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
                {
                try
                {
                        var response = await _mealDetialsServices.GetMealItems(id);
                        return Ok(response);
                }
                catch (Exception ex)
                {
                        return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
                }
            else
            {
                 return Unauthorized("you have no access to this service");
            }
        }
    //==========================================================================================================================================
    //Orders:
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetOrderDetiales([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _orderServices.GetOrderDetiales(id);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetLastOrderDetiales([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var response = await _orderServices.GetLastOrderDetiales();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateOrder([FromHeader] string token,[FromRoute]int id, [FromBody] UpdateOrderDTOs updateOrderDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    await _orderServices.UpdateOrder(id,updateOrderDTOs);
                    return StatusCode(200, "one order has been added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrder([FromHeader] string token, [FromBody] CreateOrderDTOs createOrderDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    await _orderServices.CreateOrder(createOrderDTOs);
                    return StatusCode(200, "one order has been added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> CancelOrder([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    await _orderServices.CancelOrder(id);
                    return StatusCode(200, "one order has been canceld successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
    //==========================================================================================================================================
    //Sign Up:
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddCustomer(CreatePersonDTOs person)
        {
            try
            {
                await _personServices.CreatePerson(person);
                return StatusCode(200, "one person has been added successfully");
            }
            catch(Exception ex)
            {
                return StatusCode (500, new { massage = ex.Message, ex.InnerException?.Message });
            }
            
        }
    //==========================================================================================================================================
    //Reservation:
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateReservation([FromHeader] string token, [FromBody] CreateReservationDTOs createReservationDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    await _reservationServices.CreateReservation(createReservationDTOs);
                    return StatusCode(200, "one reservation has been added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateReservation([FromHeader] string token , [FromBody] UpdateReservationDTOs updateReservationDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
                try
                {
                    await _reservationServices.UpdateReservation(updateReservationDTOs);
                    return StatusCode(200, "one reservation has been updated successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteReservation([FromHeader] string token, [FromRoute] int id )
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    await _reservationServices.DeleteRerservation(id);
                    return StatusCode(200, "one reservation has been deleted successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<IActionResult> GetMyReservation([FromHeader] string token, [FromRoute] int customerId)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                  var myReservation=  await _reservationServices.GetMyReservation(customerId);
                    return Ok(myReservation);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        //==========================================================================================================================================
        //Codes:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDiscountCode([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "customer"))
            {
                try
                {
                    var code = await _codeServices.GetDiscountCode();
                    return Ok(code);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }

    }
}