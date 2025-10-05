namespace My_Resturant.DTOs.Meal
{
    public class GetMealItemsDTOS
    {
        public int itemId { get; set; }
        public string itemName {  get; set; }
        public int quantity { get; set; }
        public string? image { get; set; }

    }
}
