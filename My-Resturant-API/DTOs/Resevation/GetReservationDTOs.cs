namespace My_Resturant.DTOs.Resevation
{
    public class GetReservationDTOs
    {
        public int reservationId { get; set; }
        public DateTime creationDate { get; set; }
        public string reservationTime { get; set; }
        public int numberOfPeople { get; set; }
        public string? status { get; set; }
        public int customerId { get; set; }
        public string? specialRequests { get; set; }
        public DateOnly reservationDate { get; set; }
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }

    }
}
