namespace My_Resturant.DTOs.Resevation
{
    public class CreateReservationDTOs
    {
        public string reservationTime { get; set; }
        public int numberOfPeople { get; set; }
        public int customerId { get; set; }
        public string? specialRequests { get; set; }
        public DateOnly reservationDate { get; set; }
        public string? customerFirstName { get; set; } //for the admin he send the name of the customer
        public string? customerLastName { get; set; } //for the admin he send the name of the customer
    }
}
