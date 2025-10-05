namespace My_Resturant.Entities
{
    public class Reservation: MainEntity
    {
        public string reservationTime { get; set; }
        public int numberOfPeople { get; set; }
        public int? status { get; set; } //null here becouse if i delete the status from lookitem set null here
        public int customerId { get; set; }
        public string? specialRequests { get; set; }
        public DateOnly ReservationDate { get; set; }

    }
}
