using My_Resturant.Entities;

namespace My_Resturant.DTOs.Meal
{
    public class GetSpecificMealDetialesDTOs:MainEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public string? image { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public List<mealDetials> mealDetials { get; set; }
    }
}
