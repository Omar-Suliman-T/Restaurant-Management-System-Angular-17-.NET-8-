using My_Resturant.DTOs.Meal;
using My_Resturant.Entities;

namespace My_Resturant.Interfaces
{
    public interface IMealDetialsServices
    {
        Task AddItemToMealOrChangeQuantity(UpdateMealDetailesDTOs updateMealDetailesDTOs);
        Task DeleteItemFromMeal(int mealId, int ItemId);
        Task DecreasItemQuantityInAMeal(UpdateMealDetailesDTOs updateMealDetailesDTOs);
        Task<List<GetMealItemsDTOS>> GetMealItems(int mealId);

    }
}
