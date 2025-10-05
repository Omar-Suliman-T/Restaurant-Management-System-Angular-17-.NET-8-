namespace My_Resturant.Entities
{
    public class Order:MainEntity
    {
        public int costumerId { get; set; }
        public int orderStatus {  get; set; }
        public string deliveryAdress { get; set; }
        public string? costumerNotes { get; set; }
        public int? rating { get; set; }
        public double netPrice { get; set; }
        public string? discountCode { get; set; }

    }
}
