namespace My_Resturant.Entities
{
    public class Meal:MainEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public string? image {  get; set; }
        public int? category { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
     }
}
