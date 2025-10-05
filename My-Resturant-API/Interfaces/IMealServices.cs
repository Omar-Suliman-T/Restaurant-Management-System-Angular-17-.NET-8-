using My_Resturant.DTOs.Meal;
using My_Resturant.Entities;

namespace My_Resturant.Interfaces
{
    public interface IMealServices
    {
        Task AddMeal(AddMealDTOs meal);
        Task<List<GetMealDTOs>> GetAllMeals();
        Task<string> UpdateMeal( int id,UpdateMealDTOs meal);
        Task DeleteMeal(int id);
        Task<GetSpecificMealDetialesDTOs> GetSpecificMealDetiales(int mealid);
    }
}
