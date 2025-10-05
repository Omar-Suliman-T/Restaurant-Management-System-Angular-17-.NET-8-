namespace My_Resturant.Entities
{
    public class Item:MainEntity
    {
        public string name {  get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int? category { get; set; }
        public string? image { get; set; }
        public List<string> ingrediants { get; set; }
        public int stock { get; set; }
    }
}
