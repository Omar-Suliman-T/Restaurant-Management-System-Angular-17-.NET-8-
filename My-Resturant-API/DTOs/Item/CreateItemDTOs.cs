namespace My_Resturant.DTOs.Item
{
    public class CreateItemDTOs
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public IFormFile? image { get; set; }
        public List<string> ingrediants { get; set; }
        public int stock { get; set; }
        public bool isActive { get; set; }
    }
}
