namespace My_Resturant.Entities
{
    public class OrderMealDetiales:MainEntity
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public int quantity { get; set; }
    }
}
