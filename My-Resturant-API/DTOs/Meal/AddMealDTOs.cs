using My_Resturant.Entities;

namespace My_Resturant.DTOs.Meal;

public class AddMealDTOs
{
    public string name { get; set; }
    public string description { get; set; }
    public IFormFile? image { get; set; }
    public int category { get; set; }
    public int stock { get; set; }
    public string details { get; set; }

}
