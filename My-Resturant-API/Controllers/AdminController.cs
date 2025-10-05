using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_Resturant.DTOs.Category;
using My_Resturant.DTOs.Ingredieants;
using My_Resturant.DTOs.Item;
using My_Resturant.DTOs.Meal;
using My_Resturant.DTOs.Order;
using My_Resturant.DTOs.Person;
using My_Resturant.DTOs.Resevation;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Implementations;
using My_Resturant.Interfaces;
using Serilog;
using System.Runtime.CompilerServices;
namespace My_Resturant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPersonServices _personServices;
        private readonly ICategoryServises _categoryServises;
        private readonly IIngrediantServices _ingrediantServices;
        private readonly IItemServices _itemServices;
        private readonly IMealDetialsServices _mealDetialsServices;
        private readonly IMealServices _mealServices;
        private readonly IOrderServices _orderServices;
        private readonly IReservationServices _ReservationServices;
        private readonly ICodeServices _codeServices;
        private readonly TokenHelper _tokenHelper;
        
        public AdminController(ICategoryServises categoryServices, IIngrediantServices ingrediantServices, IItemServices itemServices,
            IMealDetialsServices mealDetialsServices, IMealServices mealServices, IOrderServices orderServices,
            IPersonServices personServices,IReservationServices reservationServices,ICodeServices codeServices,TokenHelper TokenHelper)
        {
            this._personServices = personServices;
            this._categoryServises = categoryServices;
            this._ingrediantServices = ingrediantServices;
            this._itemServices = itemServices;
            this._mealServices = mealServices;
            this._orderServices = orderServices;
            this._mealDetialsServices = mealDetialsServices;
            this._ReservationServices = reservationServices;
            this._codeServices = codeServices;
            this._tokenHelper = TokenHelper;
        }
        //=============================================================================================================================================
        //Category Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCategories([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response = await _categoryServises.GetCategories();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { massage = ex.Message, ex.InnerException?.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetCategoryById([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var respones = await _categoryServises.GetCategoryById(id);
                    return Ok(respones);
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
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDTOs createCategoryDTOs, [FromHeader] string token)
        {       
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _categoryServises.AddCategory(createCategoryDTOs);
                    return StatusCode(200, "one category has been added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.InnerException?.Message ?? ex.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDTOs updateCategoryDTOs, [FromRoute] int id, [FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _categoryServises.UpdateCategory(id, updateCategoryDTOs);
                    return StatusCode(200, "one category has been updated successfully");
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
        public async Task<IActionResult> DeleteCategory([FromRoute] int id, [FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _categoryServises.DeleteCategory(id);
                    return StatusCode(200, "one category has been deleted successfully");
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
        //=============================================================================================================================================
        //Ingrediant Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetIngediants([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response = await _ingrediantServices.GetIngrediants();
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
        public async Task<IActionResult> GetIngrediantById([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response = await _ingrediantServices.GetIngrediantById(id);
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
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddIngrediant([FromForm] AddIngrediantsDTOs ingrediant, [FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _ingrediantServices.AddIngrediant(ingrediant);
                    return StatusCode(200, "one ingredinat has been added successfully");
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
        public async Task<IActionResult> UpdateIngrediant([FromForm] UpdateIngrediantDTOs ingrediant, [FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var dto = new UpdateIngrediantDTOs
                    {
                        name = ingrediant.name,
                        unit = ingrediant.unit,
                        isActive = ingrediant.isActive
                    };
                    await _ingrediantServices.UpdateIngrediant(id, dto, ingrediant.image);
                    return Ok("One ingredient has been updated successfully");
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
        public async Task<IActionResult> DeleteIngrediant([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _ingrediantServices.DeleteIngrediant(id);
                    return StatusCode(200, "one ingredinat has been deleted successfully");
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
        //=============================================================================================================================================
        //Item Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetItems([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
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
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetItemById([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response = await _itemServices.GetItem(id);
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
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddItem([FromHeader] string token, [FromForm] CreateItemDTOs item)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _itemServices.AddItem(item);
                    return StatusCode(200, "one item has been added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.InnerException?.Message ?? ex.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateItem([FromHeader] string token, [FromForm] UpdateItemDTOs item, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var dto = new UpdateItemDTOs
                    {
                        name = item.name,
                        price = item.price,
                        isActive = item.isActive,
                        category = item.category,
                        description = item.description,
                        ingrediants = item.ingrediants,
                        stock=item.stock,
                    };
                    await _itemServices.UpdateItem(id, dto, item.image);
                    return Ok("One ingredient has been updated successfully");
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
        public async Task<IActionResult> DeleteItem([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _itemServices.DeleteItem(id);
                    return StatusCode(200, "one item has been deleted successfully");
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
            if (await _tokenHelper.ValidateToken(token, "Admin"))
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
        //=============================================================================================================================================
        //Meal Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMeals([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
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
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetSpecificMealDetiales([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response = await _mealServices.GetSpecificMealDetiales(id);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message }) ;
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMeal([FromHeader] string token, [FromForm] AddMealDTOs meal)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _mealServices.AddMeal(meal);
                    return StatusCode(200, "one meal has been added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { massage = ex.InnerException?.Message ?? ex.Message });
                }
            }
            else
            {
                return Unauthorized("you have no access to this service");
            }
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateMeal([FromHeader] string token, [FromForm] UpdateMealDTOs meal, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _mealServices.UpdateMeal(id, meal);
                    return StatusCode(200, "one meal has been updated successfully");
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
        public async Task<IActionResult> DeleteMeal([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _mealServices.DeleteMeal(id);
                    return StatusCode(200, "one meal has been deleted successfully");
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
        public async Task<IActionResult> AddItemToMealOrChangeQuantity([FromHeader] string token, [FromBody] UpdateMealDetailesDTOs updateMealDetailesDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _mealDetialsServices.AddItemToMealOrChangeQuantity(updateMealDetailesDTOs);
                    return StatusCode(200, "one meal has been updated successfully");
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
        [Route("[action]/{mealId}/{itemId}")]
        public async Task<IActionResult> DeleteItemFromMeal([FromHeader] string token, [FromRoute] int mealId, [FromRoute] int itemId)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _mealDetialsServices.DeleteItemFromMeal(mealId, itemId);
                    return StatusCode(200, "one meal has been updated successfully");
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
        public async Task<IActionResult> DecreasItemQuantityInAMeal([FromHeader] string token, [FromBody] UpdateMealDetailesDTOs updateMealDetailesDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _mealDetialsServices.DecreasItemQuantityInAMeal(updateMealDetailesDTOs);
                    return StatusCode(200, "one meal has been updated successfully");
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
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response= await _mealDetialsServices.GetMealItems(id);
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

        //=============================================================================================================================================
        //Person Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPeople([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                   var respones= await _personServices.GetAllPeople();
                    return Ok(respones);
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
        public async Task<IActionResult> GetPersonById([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var respones = await _personServices.GetPersonById(id);
                    return Ok(respones);
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
        public async Task<IActionResult> CreatPreson([FromHeader] string token,[FromBody]CreatePersonDTOs person)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _personServices.CreatePerson(person);
                    return StatusCode(200, "one person has been added successfully");
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
        public async Task<IActionResult> UpdatePerson([FromHeader] string token,[FromBody] UpdatePersonDTOs person, [FromRoute]int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _personServices.UpdatePerson(id,person);
                    return StatusCode(200, "one person has been updated successfully");
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
        public async Task<IActionResult> DeletePerson([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _personServices.DeletePerson(id);
                    return StatusCode(200, "one person has been deleted successfully");
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
        //=============================================================================================================================================
        //Order Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrders([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    var response= await _orderServices.GetAllOrders();
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
        public async Task<IActionResult> GetOrderDetiales([FromHeader] string token, [FromRoute]int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
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
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreatOrder([FromHeader] string token, [FromBody] CreateOrderDTOs createOrderDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
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
        public async Task<IActionResult> CancelOrder([FromHeader] string token, [FromRoute]int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _orderServices.CancelOrder(id);
                    return StatusCode(200, "one order has been deleted successfully");
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
        //===========================================================================================================================================
        //Reservation Services:
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllReservations([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                  var response = await _ReservationServices.GetAllReservation();
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
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateReservation([FromHeader] string token, [FromBody] CreateReservationDTOs createReservationDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _ReservationServices.CreateReservation(createReservationDTOs);
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
        public async Task<IActionResult> UpdateReservation([FromHeader] string token, [FromBody] UpdateReservationDTOs updateReservationDTOs)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _ReservationServices.UpdateReservation(updateReservationDTOs);
                    return Ok("");
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
        public async Task<IActionResult> DeleteReservation([FromHeader] string token, [FromRoute] int id)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _ReservationServices.DeleteRerservation(id);
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
        //===========================================================================================================================================
        //codes Services:

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDiscountCode([FromHeader] string token)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                   var code=  await _codeServices.GetDiscountCode();
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateDiscountCode([FromHeader] string token, [FromBody] Code code)
        {
            if (await _tokenHelper.ValidateToken(token, "Admin"))
            {
                try
                {
                    await _codeServices.UpdateDiscountCode(code.discountCode);
                    return StatusCode(200, "the discount code has been updated successfully");
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
