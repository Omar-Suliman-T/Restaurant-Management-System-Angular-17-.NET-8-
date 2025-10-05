using My_Resturant.Entities;

namespace My_Resturant.DTOs.Item
{
    public class GetItemDTOs:MainEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string? category { get; set; }
        public string? image { get; set; }
        public List<string> ingrediants { get; set; }
        public int stock { get; set; }
    }
}
