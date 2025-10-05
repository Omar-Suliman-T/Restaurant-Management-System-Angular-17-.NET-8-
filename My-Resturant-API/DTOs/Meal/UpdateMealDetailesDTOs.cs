namespace My_Resturant.DTOs.Meal
{
    public class UpdateMealDetailesDTOs
    {
        public int mealId {  get; set; }
        public int itemId { get; set; }
        public int? quantity { get;  set; } //the number of added or deleted items
    }
}
