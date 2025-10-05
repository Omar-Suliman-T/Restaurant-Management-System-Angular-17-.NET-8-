using My_Resturant.Entities;

namespace My_Resturant.DTOs.Meal
{
    public class UpdateMealDTOs
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public IFormFile? image { get; set; }
        public int? category { get; set; }
        public int? stock { get; set; }
        public bool ? isActive { get; set; }
    }
}
