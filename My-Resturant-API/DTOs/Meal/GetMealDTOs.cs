namespace My_Resturant.DTOs.Meal
{
    public class GetMealDTOs
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string? image { get; set; }
        public string? category { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
    }
}

